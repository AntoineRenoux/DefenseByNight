using System.ComponentModel.DataAnnotations;
using DefenseByNight.API.Helpers;

namespace DefenseByNight.API.Models
{
    public class CharacterViewModel
    {
        public string CharacterKey { get; set; }

        [Required]
        [RegularExpression(ValidationsRegex.FirstNameRegex, ErrorMessage="ERR_FIRSTNAME_INCORRECT")]
        public string Name { get; set; }

        [Required]
        public AffiliateViewModel Sect { get; set; }
    }
}