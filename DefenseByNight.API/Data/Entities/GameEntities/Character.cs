using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefenseByNight.API.Data.Entities
{
    public class Character
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CharacterKey { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Affiliate Sect { get; set; }

        [Required]
        public Chronicle Chronicle { get; set; }

        [Required]
        public int PhysicalAttribut { get; set; } = 1;

        [Required]
        public int SocialAttribut { get; set; } = 1;

        [Required]
        public int MentalAttribut { get; set; } = 1;
    }
}