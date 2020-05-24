using System.ComponentModel.DataAnnotations;

namespace DefenseByNight.API.Data.Entities
{
    public class Focus
    {
        [Key]
        public string FocusKey { get; set; }

        [Required]
        public string FocusName { get; set; }

        [Required]
        public string Description { get; set; }
    }
}