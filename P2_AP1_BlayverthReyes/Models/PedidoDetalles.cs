using System.ComponentModel.DataAnnotations;

namespace P2_AP1_BlayverthReyes.Models
{
    public class PedidoDetalles
    {
        [Key]
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ComponenteId { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
    }
}
