using System.ComponentModel.DataAnnotations;

namespace DefenseByNight.API.Data.Entities
{
    public class Traduction
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public int LCID { get; set; }

        [Required]
        public string Text { get; set; }
    }
}