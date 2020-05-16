using System.ComponentModel.DataAnnotations;

namespace DefenseByNight.API.Data.Entities
{
    public class Flaw
    {
        [Key]
        public string FlawKey { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Cost { get; set; }

    }
}