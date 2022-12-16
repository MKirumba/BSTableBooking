using System.ComponentModel.DataAnnotations;

namespace BSTableBooking.Models
{
    public class ChangePasswordModel
    {
        /// <summary>
        /// Model class for changing password
        /// </summary>
        [Required]
        public string? CurrentPassword { get; set; }
        /// <summary>
        /// Check for password is acceptable format
        /// </summary>
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Minimum length 6 and must contain  1 Uppercase,1 lowercase, 1 special character and 1 digit")]
        public string? NewPassword { get; set; }
        [Required]
        [Compare("NewPassword")]
        public string? PasswordConfirm { get; set; }
    }
}
