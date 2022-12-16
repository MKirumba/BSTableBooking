using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BSTableBooking.Models
{
    public class AvailTables
    {

        /// <summary>
        /// Availtable is Model for tracking tables / seats availabe for each sitting /session
        /// </summary>
        [Required]
        [DisplayName("Available Seats")]
        public int Qty { get; set; }


        //public DateTime LastUpdatedDate { get; set; }= DateTime.Now;


        [Key]
        public int SessionID { get; set; }
        public Session AvailSession { get; set; }


        public List<Booking> Booking { get; set; }

    }
}
