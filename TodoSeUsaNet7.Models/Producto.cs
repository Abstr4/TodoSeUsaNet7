using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TodoSeUsaNet7.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(100, ErrorMessage = "Longitud máxima de {0} carácteres")]
        public string Tipo { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Longitud máxima de {0} carácteres")]
        public string? Descripcion { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Condicion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(25, ErrorMessage = "Campo obligatorio")]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obligatorio")]
        [Range(0, Int32.MaxValue, ErrorMessage = "El {0} tiene que ser mayor a 0.")]
        public int Precio { get; set; }
        public bool Reacondicionado { get; set; } = false;

        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "Costo de reacondicionamiento")]
        [Range(0, Int32.MaxValue, ErrorMessage = "El {0} tiene que ser mayor a 0.")]
        public int CostoReacondicionamiento { get; set; } = -1;
        public bool Devolver { get; set; } = false;
        public bool Devuelto { get; set; } = false;
        public bool Sold { get; set; } = false;
        public bool Active { get; set; } = true;

        // Relations

        [ForeignKey("FacturaId")]
        public int FacturaId { get; set; }
        public virtual Factura? Factura { get; set; }
    }
}
