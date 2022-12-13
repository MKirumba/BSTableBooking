using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BSTableBooking.Models
{
    public class TableArea
    {
        [Key]
        public int TableAreaID { get; set; }
        [Required]
        [DisplayName("Table Location")]
        public string TableAreaName { get; set; }
        [DisplayName("Table Description")]
        public string? TableAreaDescription { get; set; }

        public List<Session> Session { get; set; } = new List<Session>();


    }
}
