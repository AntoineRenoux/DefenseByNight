using System.ComponentModel.DataAnnotations;
using static DefenseByNight.API.Helpers.Enums.EnumAtoutFlaw;

namespace DefenseByNight.API.Data.Entities
{
    public class Atout
    {
        [Key]
        public string AtoutKey { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public TypeAtout Type { get; set; }

        public virtual Clan Clan { get; set; }

        [Required]
        public int Cost { get; set; }
    }
}