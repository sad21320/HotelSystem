using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSystem.Models
{
    public class AdditionalService
    {
        [Key]                                 
        public int ServiceId { get; set; }

        [Required]
        public string ServiceName { get; set; } = null!;

        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<InvoiceService> InvoiceServices { get; set; } = new List<InvoiceService>();
    }
}