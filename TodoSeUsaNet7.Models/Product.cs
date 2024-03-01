using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TodoSeUsaNet7.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Max. Length of {0}")]
        public string Type { get; set; }

        [MaxLength(500, ErrorMessage = "Max. Length of {0}")]
        public string? Description { get; set; } 

        [MaxLength(500)]
        public string? Condition { get; set; } 

        [Required]
        [MaxLength(25, ErrorMessage = "Max. Length of {0}")]
        public string State { get; set; } 

        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public int Price { get; set; }

        public bool Reaconditioned { get; set; }

        [Required]
        [Display(Name = "Costo de reacondicionamiento")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Cost must be greater than 0")]
        public int ReaconditioningCost { get; set; }
        public bool MustReturn { get; set; } 
        public bool Returned { get; set; } 
        public bool Sold { get; set; }
        public bool Active { get; set; }

        // Relations

        [ForeignKey("BillId")]
        public int BillId { get; set; }
        public virtual Bill? Bill { get; set; }
    }
}
