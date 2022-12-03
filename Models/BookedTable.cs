using System.ComponentModel.DataAnnotations;

namespace BSTableBooking.Models
{
    public class BookedTable
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        
        public List<BookingInfo> Product { get; set; } = new List<BookingInfo>();
    }
}
