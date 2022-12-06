using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BSTableBooking.Models
{
    public class TableArea
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [DisplayName("Table Location")]
        public string CategoryName { get; set; }
        [DisplayName("Table Description")]
        public string? CategoryDescription { get; set; }
        
        public List<BookingInfo> Product { get; set; } = new List<BookingInfo>();
    }
}
