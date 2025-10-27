using System.ComponentModel.DataAnnotations;

namespace P2_AP1_BlayverthReyes.Models;

public class Modelos
{
    [Key]
    public int Id { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;
}
