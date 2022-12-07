using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSTableBooking.Models
{
    public class AvailTables
    {
        [Key]
        [Required]
        public int ProductId { get; set; }
        [Required]
        [DisplayName("Available Seats")]
        public int Qty { get; set; }
        public DateTime LastUpdatedDate { get; set; }= DateTime.Now;
       
        public BookingInfo Product{ get; set; }


        [Required]
        public DateTime BookDay { get; set; } = DateTime.Now;
       


    }
}
