using System.ComponentModel.DataAnnotations;

namespace DefenseByNight.API.Data.Entities
{
    public class Reference
    {
        [Key]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

    }
}