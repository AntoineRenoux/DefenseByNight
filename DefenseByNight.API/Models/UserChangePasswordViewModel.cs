using System.ComponentModel.DataAnnotations;

namespace DefenseByNight.API.Models
{
    public class UserChangePasswordViewModel
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}