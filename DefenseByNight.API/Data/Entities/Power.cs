using System.ComponentModel.DataAnnotations;

namespace DefenseByNight.API.Data.Entities
{
    public class Power
    {
        [Key]
        public string PowerKey { get; set; }

        [Required]
        public string PowerName { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public string System { get; set; }

        public string Description { get; set; }

        public Focus Focus { get; set; }

        public string FocusEffect { get; set; }

        public string ExceptionalSuccess { get; set; }
    }
}