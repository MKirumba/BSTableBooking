 using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSTableBooking.Models
{
    public class Booking
    {
        
        [Key]
        public int BookingID { get; set; }

        
        //[DisplayName("Session")]
        //public int? SessionID { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;

        [DisplayName("Customer Name")]
        public string BookingDescription { get; set; }
        [Required]
        [DisplayName("Number of Guests")] 
        public int Qty { get; set; }

      
        public AvailTables SessionID { get; set; }
       // public Session Session { get; set; }

        //[ForeignKey("SessionID")]
        //public List<Session> Session { get; set; }


        //MK added


        [Required]
        [DisplayName("Start Time")]
        public DateTime BookingStartTime { get; set; } = DateTime.Now;
        [Required]
        [DisplayName("End Time")]
        public DateTime BookingEndTime { get; set; } = DateTime.Now;
        [Required]
        [DisplayName("Booking Duration")]
        public int BookingDuration { get; set; }
        [DisplayName("Booking Source")]
        public string? BookingSource { get; set; }
        [DisplayName("Booking Notes")]
        public string? BookingNotes { get; set; }
        [DisplayName("Booking Status")]
        public string? BookingStatus { get; set; }

        [NotMapped]
        public string BookingTableLocation { get; set; }




















    }
}
