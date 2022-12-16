using System.ComponentModel.DataAnnotations;

namespace BSTableBooking.Models
{
    public class LoginModel
    {
        /// <summary>
        /// Model for login info
        /// </summary>
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
