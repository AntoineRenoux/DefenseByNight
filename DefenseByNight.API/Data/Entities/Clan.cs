using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DefenseByNight.API.Helpers.Enums;

namespace DefenseByNight.API.Data.Entities
{
    public class Clan
    {
        [Key]
        public string ClanKey { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string History { get; set; }

        [Required]
        public string Organisation { get; set; }

        [Required]
        public string Weakness { get; set; }

        [Required]
        public EnumRarityClan RarityClan { get; set; }

        [Required]
        public string Affiliate { get; set; }

        public virtual ICollection<Discipline> Disciplines { get; set; }
        
    }
}