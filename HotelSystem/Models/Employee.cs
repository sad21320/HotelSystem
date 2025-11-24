using System.ComponentModel.DataAnnotations;

namespace HotelSystem.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string Position { get; set; } = null!; // Администратор, Горничная и т.д.
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
