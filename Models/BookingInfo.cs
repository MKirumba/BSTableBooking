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
        public int CategoryID { get; set; }

        public TableArea Category { get; set; }
        public AvailTables Stock { get; set; }
        public List<Booking> Order { get; set; } = new List<Booking>();
        

        [NotMapped]
        [Required]
        public int Categories { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? CategoryList { get; set; }
        [NotMapped]
        public string? CategoryNames { get; set; }
        [NotMapped]
        public int? Qty { get; set; }

        //[NotMapped]
        //public int? Qty { get; set; }



        // MK Add
        // Add in Session date (MK)

        [Required]
        [DisplayName("Session Start Time")]
        public DateTime SessionStartTime { get; set; }
        [Required]
        [DisplayName("Session End Time")]
        public DateTime SessionEndTime { get; set; }
        [DisplayName("Booking Session")]
        public string? BookingSession { get; set; }

        

    }
}
