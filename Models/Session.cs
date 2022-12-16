using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSTableBooking.Models
{
    public class Session
    {
        /// <summary>
        /// Model class for available Sessions / Seatings 
        /// /// </summary>
        [Key]
        public int SessionID { get; set; }
        [Required]
        [DisplayName("Session Name")]
        public string SessionName { get; set; }
        [Required]
        [DisplayName("Session Description")]
        public string SessionDescription { get; set; }
        [Required]
        [DisplayName("Seats Capacity")]
        public int Qty { get; set; }

        public int? TableAreaID { get; set; }

        public TableArea? TableArea { get; set; }
        public AvailTables? FreeTables { get; set; }

        //[ForeignKey("BookingID")]
        //public Booking BookedID { get; set; }

        //public Booking Booked { get; set; }


        [NotMapped]
        [Required]
        public int TableAreas { get; set; }
        [NotMapped]
        public virtual IEnumerable<SelectListItem>? TableAreaList { get; set; }

        [NotMapped]
        public string? TableAreaNames { get; set; }
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

        [DisplayName("Booking Session")]
        public string? BookingSession { get; set; }
        [Required]
        [DisplayName("Seating Location")]
        public string? TableLocation { get; set; }

        [DisplayName("Cover Image")]
        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
