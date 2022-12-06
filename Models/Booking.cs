﻿ using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSTableBooking.Models
{
    public class Booking
    {
        
        [Key]
        public int OrderID { get; set; }
        
        [Required]
        [DisplayName("Session")]
        public int ProductID { get; set; }
        public DateTime OrderDate { get; set; }= new DateTime();

        [DisplayName("Customer Name")]
        public string OrderDescription { get; set; }
        [Required]
        [DisplayName("Number of Guests")]
        public int Qty { get; set; }

        public BookingInfo Product { get; set; }

        //MK added

        [DisplayName("Start Time")]
        public DateTime BookingStartTime { get; set; } = DateTime.Now;
        [DisplayName("End Time")]
        public DateTime BookingEndTime { get; set; } = DateTime.Now;
        [DisplayName("Booking Duration")]
        public int BookingDuration { get; set; }
        [DisplayName("Booking Source")]
        public string? BookingSource { get; set; }
        [DisplayName("Booking Notes")]
        public string? BookingNotes { get; set; }
        [DisplayName("Booking Status")]
        public string? BookingStatus { get; set; }


















    }
}
