using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSTableBooking.Models
{
    public class Booking
    {
        /// <summary>
        /// /Booking is the booking inforamtion
        /// </summary>
        [Key]
        public int BookingID { get; set; }

        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; } = DateTime.Now;

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }
        [Required]
        [DisplayName("Number of Guests")]
        public int Qty { get; set; }

        public int? SessionID { get; set; }

        public List<AvailTables> Session { get; set; }

        [DisplayName("Start Time")]
        public DateTime? BookingStartTime { get; set; } = DateTime.Now;

        [DisplayName("End Time")]
        public DateTime? BookingEndTime { get; set; } = DateTime.Now;

        [DisplayName("Booking Duration")]
        public int? BookingDuration { get; set; }
        [DisplayName("Booking Source")]
        public string? BookingSource { get; set; }
        [DisplayName("Booking Notes")]
        public string? BookingNotes { get; set; }
        [DisplayName("Booking Status")]
        public string? BookingStatus { get; set; }

        public string? CustomerIdentity { get; set; }


        [NotMapped]
        public string? BookingTableLocation { get; set; }




















    }
}
