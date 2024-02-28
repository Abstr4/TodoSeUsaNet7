
using System.ComponentModel.DataAnnotations;

namespace TodoSeUsaNet7.Models
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }

        [Display(Name = "Fecha de Emisión")]
        public DateTime DateCreated { get; set; }
        public int TotalProducts { get; set; }
        public int? ProductsSold { get; set; }
        public int TotalAmountPerProducts { get; set; }
        public int? TotalAmountSold { get; set; }
        public bool Closed { get; set; } = false;
        public bool Active { get; set; } = true;

        // Relations
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }

        public virtual ICollection<Product>? Product { get; set; }

    }
}
