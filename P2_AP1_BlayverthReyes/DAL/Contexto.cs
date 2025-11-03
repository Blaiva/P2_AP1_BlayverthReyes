using Microsoft.EntityFrameworkCore;
using P2_AP1_BlayverthReyes.Models;

namespace P2_AP1_BlayverthReyes.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions options) : base(options) {}

    public DbSet<Pedidos> Pedidos { get; set; }
    public DbSet<PedidoDetalles> PedidoDetalles { get; set; }
    public DbSet<Componentes> Componentes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Componentes>(entity =>
        {
            entity.HasData(
                new Componentes
                {
                    ComponenteId = 1,
                    Descripcion = "Memoria 4GB",
                    Precio = 1580,
                    Existencia = 1
                },
                new Componentes
                {
                    ComponenteId = 2,
                    Descripcion = "Disco SSD 120MB",
                    Precio = 4200,
                    Existencia = 8
                },
                new Componentes
                {
                    ComponenteId = 3,
                    Descripcion = "Tarjeta de Video",
                    Precio = 10000,
                    Existencia = 4
                }
            );
        });
    }
}
