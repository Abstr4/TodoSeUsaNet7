using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TodoSeUsaNet7.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Obligatory field")]
        [MaxLength(100, ErrorMessage = "Max. Length of {0}")]
        public string Type { get; set; }

        [MaxLength(500, ErrorMessage = "Max. Length of {0}")]
        public string? Descripcion { get; set; } 

        [MaxLength(500)]
        public string? Condicion { get; set; } 

        [Required(ErrorMessage = "Obligatory field")]
        [MaxLength(25, ErrorMessage = "Max. Length of {0}")]
        public string Estado { get; set; } 

        [Required(ErrorMessage = "Obligatory field")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public int Precio { get; set; }

        public bool Reacondicionado { get; set; } = false;

        [Required(ErrorMessage = "Obligatory field")]
        [Display(Name = "Costo de reacondicionamiento")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Cost must be greater than 0")]
        public int CostoReacondicionamiento { get; set; }
        public bool Devolver { get; set; } 
        public bool Devuelto { get; set; } 
        public bool Sold { get; set; }
        public bool Active { get; set; }

        // Relations

        [ForeignKey("BillId")]
        public int BillId { get; set; }
        public virtual Bill? Bill { get; set; }
    }
}
