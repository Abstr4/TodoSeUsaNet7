using System.ComponentModel.DataAnnotations;

namespace TodoSeUsaNet7.Models
{
    public class Client
    {
        [Key]
        [Display(Name = "Cód. del cliente")]
        public int ClientId { get; set; }

        [Required]
        [MaxLength(64)]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(64)]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [MaxLength(64)]
        [Display(Name = "Num. Telefono")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El número de teléfono solo puede contener números.")]
        public string? PhoneNumber { get; set; }

        [MaxLength(64)]
        [Display(Name = "Dirección")]
        public string? Address { get; set; }

        [Required]
        [MaxLength(64)]
        public string Email { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }

        [Display(Name = "Facturas totales")]
        public int TotalBills { get; set; }

        [Display(Name = "Prouctos totales")]
        public int TotalProducts { get; set; }

        [Display(Name = "Productos vendidos")]
        public int ProductsSold { get; set; }

        [Display(Name = "Monto total por productos")]
        public int TotalAmountPerProducts { get; set; }

        [Display(Name = "Monto total vendido")]
        public int TotalAmountSold { get; set; }

        // Relations
        public virtual ICollection<Bill>? Bills { get; set; }
    }
}
