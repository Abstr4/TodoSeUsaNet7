using System.ComponentModel.DataAnnotations;

namespace TodoSeUsaNet7.Models
{
    public class Venta
    {
        [Key]
        public int VentaId { get; set; }

        [Display(Name = "Fecha de Emisión")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string Detalle { get; set; }
        public int Monto { get; set; }
        public int Entrega { get; set; }
        public int Debe { get; set; }
        public int ProductosTotales { get; set; }
        public bool Closed { get; set; }
        public bool Active { get; set; } = true;

        public int ClienteId { get; set; }
        public virtual Cliente? Cliente { get; set; }

        public virtual ICollection<Producto>? Productos { get; set; }
        public virtual ICollection<Factura>? Facturas { get; set; }



    }
}
