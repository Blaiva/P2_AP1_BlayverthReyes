using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2_AP1_BlayverthReyes.Models;

public class Pedidos
{
    [Key]
    public int Id { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El nombre del cliente es requerido")]
    public string NombreCliente { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor que 0")]
    public double Total { get; set; }

    [ForeignKey("PedidoId")]
    public virtual ICollection<PedidoDetalles> Detalles { get; set; } = new List<PedidoDetalles>();
}
