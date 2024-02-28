using System.ComponentModel.DataAnnotations;

namespace TodoSeUsaNet7.Models
{
    public class Client
    {
        [Key]
        public int ClienteId { get; set; }

        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(64)]
        public string LastName { get; set; }

        [MaxLength(64)]
        public string? PhoneNumber { get; set; }

        [MaxLength(64)]
        public string? Address { get; set; }

        [Required]
        [MaxLength(64)]
        public string Email { get; set; }

        public bool Active { get; set; }
        public int TotalBills { get; set; }
        public int TotalProducts { get; set; }
        public int? ProductsSold { get; set; }
        public int TotalAmountPerProducts { get; set; }
        public int? TotalAmountSold { get; set; }

        // Relations
        public virtual ICollection<Bill>? Bills { get; set; }
    }
}
