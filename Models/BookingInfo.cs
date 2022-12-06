using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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
        [DisplayName("Session Name")]
        public string ProductName { get; set; }
        [Required]
        [DisplayName("Session Description")]
        public string ProductDescription { get; set; }
        [Required]
        [DisplayName("Seats Capacity")]
        public int Qty { get; set; }

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


        // MK Add
        // Add in Session date (MK)

        [Required]
        [DisplayName("Session Start Time")]
        [DataType(DataType.Date)]
        public DateTime SessionStartTime { get; set; } = DateTime.Now;
        [Required]
        [DisplayName("Session End Time")]
        [DataType(DataType.Date)]
        public DateTime SessionEndTime { get; set; } = DateTime.Now;
        [Required]
        [DisplayName("Booking Session")]
        public string? BookingSession { get; set; }
        [Required]
        [DisplayName("Seating Location")]
        public string TableLocation { get; set; }



    }
}
