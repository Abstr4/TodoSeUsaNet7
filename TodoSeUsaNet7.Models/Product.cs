using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TodoSeUsaNet7.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "Cód. del producto")]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(256, ErrorMessage = "Longitud máxima de {0}")]
        [Display(Name = "Tipo")]
        public string Type { get; set; }

        [MaxLength(500, ErrorMessage = "Longitud máxima de {0}")]
        [Display(Name = "Descripción")]
        public string? Description { get; set; } 

        [MaxLength(256)]
        [Display(Name = "Condición")]
        public string? Condition { get; set; } 

        [Required]
        [MaxLength(256, ErrorMessage = "Longitud máxima de {0}")]
        [Display(Name = "Estado")]
        public string State { get; set; } 

        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "El precio debe ser mayor a 0 (cero)")]
        [Display(Name = "Precio")]
        public int Price { get; set; }

        [Display(Name = "Reacondicionado")]
        public bool Reaconditioned { get; set; }

        [Display(Name = "Costo de reacondicionamiento")]
        [Range(0, Int32.MaxValue, ErrorMessage = "El costo debe ser mayor a 0 (cero)")]
        public int? ReaconditioningCost { get; set; }

        [Display(Name = "Devolver")]
        public bool MustReturn { get; set; }

        [Display(Name = "Devuelto")]
        public bool Returned { get; set; }

        [Display(Name = "Vendido")]
        public bool Sold { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }

        // Relations

/*        [ForeignKey("BillId")]*/
        public int BillId { get; set; }
        public virtual Bill? Bill { get; set; }
    }
}
