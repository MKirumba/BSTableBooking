using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSTableBooking.Models
{
    public class BookingInfo
    {
        [Key]
        public int ProuctId { get; set; }
        [Required]
        [DisplayName("BookingInfo Name")]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public string Image { get; set; }
        public int CategoryID { get; set; }
        
        public BookedTable Category { get; set; }
        public AvailTables Stock { get; set; }
        public List<Booking> Order { get; set; } = new List<Booking>();
        

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [NotMapped]
        [Required]
        // public List<int>? Categories { get; set; }
        public int Categories { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? CategoryList { get; set; }
        [NotMapped]
        public string? CategoryNames { get; set; }
        [NotMapped]
        public int? Qty { get; set; }
    }
}
