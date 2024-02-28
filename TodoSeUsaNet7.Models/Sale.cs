using System.ComponentModel.DataAnnotations;

namespace TodoSeUsaNet7.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [Display(Name = "Date of issue")]
        public DateTime DateOfIssue { get; set; } = DateTime.Now;
        public string Detail { get; set; }
        public int Amount { get; set; }
        public int Entrega { get; set; }
        public int Debe { get; set; }
        public int TotalProducts { get; set; }
        public bool Closed { get; set; }
        public bool Active { get; set; } = true;

        // Navigation properties
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
