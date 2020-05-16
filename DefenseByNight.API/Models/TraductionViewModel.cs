using System.ComponentModel.DataAnnotations;

namespace DefenseByNight.API.Models
{
    public class TraductionViewModel
    {
        [Required]
        public string Key { get; set; }

        public string Text { get; set; }

    }
}