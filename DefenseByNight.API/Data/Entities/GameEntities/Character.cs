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
    }
}