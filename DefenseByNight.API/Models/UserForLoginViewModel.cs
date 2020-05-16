using System.ComponentModel.DataAnnotations;

namespace DefenseByNight.API.Models
{
    public class UserForLoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}