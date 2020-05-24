using System.ComponentModel.DataAnnotations;

namespace DefenseByNight.API.Models
{
    public class HealthAddViewModel
    {
        public int Id { get; set; }
        public string Allergies { get; set; }
        [Required]
        public string ContactFirstName { get; set; }
        [Required]
        public string ContactLastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}