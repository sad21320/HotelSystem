using System.ComponentModel.DataAnnotations;

namespace HotelSystem.Models
{
    public class RoomType
    {
        public int RoomTypeID { get; set; }
        public string TypeName { get; set; } = null!;
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public decimal BasePrice { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
