using MessagePack;
using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BSTableBooking.Models
{
    public class AvailTables
    {

        [System.ComponentModel.DataAnnotations.Key]
        public int ProductId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [DisplayName("Available Seats")]
        public int Qty { get; set; }
        public DateTime LastUpdatedDate { get; set; }= DateTime.Now;
        
        public BookingInfo Product{ get; set; }
    }
}
