using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Newtonsoft.Json;
using NuGet.Frameworks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace BSTableBooking.Models
{
    public class AvailTables
    {
        [Key]
        public int SessionID { get; set; }

        //[Required]
        //[ForeignKey("BookingID")]
        //public Booking Booked { get; set; }

        // public int? SessionId { get; set; }
        [Required]
        [DisplayName("Available Seats")]
        public int Qty { get; set; }
        public DateTime LastUpdatedDate { get; set; }= DateTime.Now;

        // Check name change ref
        // Dropped for namechange migration
        public Session AvailSession { get; set; }

        // Many to Many Bookings to AvailTables
        //public List<Booking> BridgeBookingID { get; set; }

        //One booking per session
        [ForeignKey("SessionID")]
        public Booking bkSessionID { get; set; }



        [Required]
        [DataType(DataType.Date)]
        public DateTime BookDay { get; set; }

        [Required]
        public string SessionSlot { get; set; }




    }
}
