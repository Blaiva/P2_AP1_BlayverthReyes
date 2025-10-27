using Microsoft.EntityFrameworkCore;
using P2_AP1_BlayverthReyes.Models;

namespace P2_AP1_BlayverthReyes.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions options) : base(options) {}

    public DbSet<Modelos> Modelos { get; set; }
}
