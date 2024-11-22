
using System.ComponentModel.DataAnnotations;

namespace TodoSeUsaNet7.Models
{
    public class Bill
    {
        [Key]
        [Display(Name = "Cód. Factura")]
        public int BillId { get; set; }

        [Display(Name = "Fecha de Emisión")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Prouctos totales")]
        public int TotalProducts { get; set; }

        [Display(Name = "Productos vendidos")]
        public int ProductsSold { get; set; }

        [Display(Name = "Monto total por productos")]
        public int TotalAmountPerProducts { get; set; }

        [Display(Name = "Monto total vendido")]
        public int TotalAmountSold { get; set; }

        [Display(Name = "Cerrada")]
        public bool Closed { get; set; }

        [Display(Name = "Activa")]
        public bool Active { get; set; } 

        // Relations
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }

        public virtual ICollection<Product>? Products { get; set; }

    }
}
