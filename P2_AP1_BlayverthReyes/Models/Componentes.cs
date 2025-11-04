using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P2_AP1_BlayverthReyes.Models;

public class Componentes
{
    [Key]
    public int ComponenteId { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public double Precio { get; set; }
    public int Existencia { get; set; }
}
