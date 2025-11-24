using HotelSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelSystem.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; } = null!;
        public int RoomTypeID { get; set; }
        public int Floor { get; set; }
        public string Status { get; set; } = "Available"; // Available, Booked, Cleaning
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public RoomType RoomType { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
