
using System.ComponentModel.DataAnnotations;

namespace TodoSeUsaNet7.Models
{
    public class Factura
    {
        [Key]
        public int FacturaId { get; set; }

        [Display(Name = "Fecha de Emisión")]
        public DateTime FechaCreacion { get; set; }
        public int ProductosTotales { get; set; }
        public int? ProductosVendidos { get; set; }
        public int MontoTotalPorProductos { get; set; }
        public int? MontoTotalVendido { get; set; }
        public bool Closed { get; set; } = false;
        public bool Active { get; set; } = true;

        // Relations
        public int ClienteId { get; set; }
        public virtual Cliente? Cliente { get; set; }

        public virtual ICollection<Producto>? Producto { get; set; }

    }
}
