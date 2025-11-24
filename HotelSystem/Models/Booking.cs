using System.ComponentModel.DataAnnotations;

namespace HotelSystem.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }

        public int GuestID { get; set; }
        public int RoomID { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Confirmed";
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Навигационные свойства
        public Guest Guest { get; set; } = null!;
        public Room Room { get; set; } = null!;
    }
}