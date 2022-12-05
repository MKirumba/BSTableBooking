using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSTableBooking.Models
{
    public class Booking
    {
        
        [Key]
        public int OrderID { get; set; }
        
        [Required]
        public int ProductID { get; set; }
        public DateTime OrderDate { get; set; }=new DateTime();
        public string OrderDescription { get; set; }
        [Required]
        public int Qty { get; set; }

        public BookingInfo Product { get; set; }
    }
}
