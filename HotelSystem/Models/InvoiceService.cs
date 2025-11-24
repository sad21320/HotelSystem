namespace HotelSystem.Models
{
    public class InvoiceService
    {
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;

        public int ServiceId { get; set; }
        public AdditionalService Service { get; set; } = null!;

        public int Quantity { get; set; } = 1;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
