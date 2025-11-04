using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using P2_AP1_BlayverthReyes.DAL;
using P2_AP1_BlayverthReyes.Models;

namespace P2_AP1_BlayverthReyes.Services;

public class PedidosService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pedidos.AnyAsync(e => e.Id == id);
    }

    public async Task AfectarExistencia(PedidoDetalles[] detalle, TipoOperacion tipoOperacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        foreach (var item in detalle)
        {
            var componente = await contexto.Componentes.SingleAsync(t => t.ComponenteId == item.ComponenteId);
            if (tipoOperacion == TipoOperacion.Suma)
                componente.Existencia += item.Cantidad;
            else
                componente.Existencia -= item.Cantidad;
            await contexto.SaveChangesAsync();
        }
    }

    public async Task<bool> Insertar(Pedidos pedido)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Pedidos.Add(pedido);
        await AfectarExistencia(pedido.Detalles.ToArray(), TipoOperacion.Resta);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Pedidos pedido)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var original = await contexto.Pedidos
            .Include(e => e.Detalles)
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == pedido.Id);

        if (original == null) return false;

        await AfectarExistencia(original.Detalles.ToArray(), TipoOperacion.Suma);

        contexto.PedidoDetalles.RemoveRange(original.Detalles);

        contexto.Update(pedido);

        await AfectarExistencia(pedido.Detalles.ToArray(), TipoOperacion.Resta);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Pedidos pedido)
    {
        if (!await Existe(pedido.Id))
            return await Insertar(pedido);
        else
            return await Modificar(pedido);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var pedido = await Buscar(id);

        await AfectarExistencia(pedido.Detalles.ToArray(), TipoOperacion.Resta);
        contexto.PedidoDetalles.RemoveRange(pedido.Detalles);
        contexto.Pedidos.Remove(pedido);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Pedidos?> Buscar(int Id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pedidos.Include(e => e.Detalles).FirstOrDefaultAsync(e => e.Id == Id);
    }

    public async Task<List<Pedidos>> Listar(Expression<Func<Pedidos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pedidos.Include(e => e.Detalles).Where(criterio).AsNoTracking().ToListAsync();
    }

    public async Task<List<Componentes>> ListarComponentes()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Componentes.Where(c => c.ComponenteId > 0).AsNoTracking().ToListAsync();
    }
}

public enum TipoOperacion
{
    Suma = 1,
    Resta = 2
}
