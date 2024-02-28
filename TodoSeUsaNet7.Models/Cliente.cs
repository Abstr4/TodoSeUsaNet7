using System.ComponentModel.DataAnnotations;

namespace TodoSeUsaNet7.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required]
        [MaxLength(256)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(256)]
        public string Apellido { get; set; }

        [MaxLength(256)]
        public string? Telefono { get; set; }

        [MaxLength(256)]
        public string? Direccion { get; set; }

        [Required]
        [MaxLength(256)]
        public string? Email { get; set; }

        public bool Active { get; set; }
        public int FacturasTotales { get; set; }
        public int ProductosTotales { get; set; }
        public int? ProductosVendidos { get; set; }
        public int MontoTotalPorProductos { get; set; }
        public int? MontoTotalVendido { get; set; }

        // Relations
        public virtual ICollection<Factura>? Facturas { get; set; }


    }
}
