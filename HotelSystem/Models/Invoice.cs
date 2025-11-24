using HotelSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelSystem.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        public int BookingId { get; set; }
        public int? EmployeeId { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Booking Booking { get; set; } = null!;
        public Employee? Employee { get; set; }
        public ICollection<InvoiceService> InvoiceServices { get; set; } = new List<InvoiceService>();
    }
}
