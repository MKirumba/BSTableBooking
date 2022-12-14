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
        
      

        //[Required]
        //[ForeignKey("BookingID")]
        //public Booking Booked { get; set; }

        // public int? SessionId { get; set; }
        [Required]
        [DisplayName("Available Seats")]
        public int Qty { get; set; }


        //public DateTime LastUpdatedDate { get; set; }= DateTime.Now;


        [Key]
        public int SessionID { get; set; }
        public Session AvailSession { get; set; }


        public Booking Booking { get; set; } 
       
    }
}
