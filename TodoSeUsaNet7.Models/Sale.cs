using System.ComponentModel.DataAnnotations;

namespace TodoSeUsaNet7.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Date of issue")]
        public DateTime DateOfIssue { get; set; } = DateTime.Now;
        public string Detail { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int Paid { get; set; }
        [Required]
        public int Owes { get; set; }

        [Required]
        public int TotalProducts { get; set; }
        public bool Closed { get; set; }
        public bool Active { get; set; }

        // Navigation properties
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
