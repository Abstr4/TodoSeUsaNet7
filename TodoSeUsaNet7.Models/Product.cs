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
        [MaxLength(100, ErrorMessage = "Max. Length of {0}")]
        [Display(Name = "Tipo")]
        public string Type { get; set; }

        [MaxLength(500, ErrorMessage = "Max. Length of {0}")]
        [Display(Name = "Descripción")]
        public string? Description { get; set; } 

        [MaxLength(500)]
        [Display(Name = "Condición")]
        public string? Condition { get; set; } 

        [Required]
        [MaxLength(25, ErrorMessage = "Max. Length of {0}")]
        [Display(Name = "Estado")]
        public string State { get; set; } 

        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Display(Name = "Precio")]
        public int Price { get; set; }

        [Display(Name = "Reacondicionado")]
        public bool Reaconditioned { get; set; }

        [Required]
        [Display(Name = "Costo de reacondicionamiento")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Cost must be greater than 0")]
        public int ReaconditioningCost { get; set; }

        [Display(Name = "Devolver")]
        public bool MustReturn { get; set; }

        [Display(Name = "Devuelto")]
        public bool Returned { get; set; }

        [Display(Name = "Vendido")]
        public bool Sold { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }

        // Relations

        [ForeignKey("BillId")]
        public int BillId { get; set; }
        public virtual Bill? Bill { get; set; }
    }
}
