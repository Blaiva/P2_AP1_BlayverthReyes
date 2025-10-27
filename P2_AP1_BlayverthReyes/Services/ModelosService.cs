using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using P2_AP1_BlayverthReyes.DAL;
using P2_AP1_BlayverthReyes.Models;

namespace P2_AP1_BlayverthReyes.Services;

public class ModelosService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<List<Modelos>> Listar(Expression<Func<Modelos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Modelos.Where(criterio).AsNoTracking().ToListAsync();
    }
}
