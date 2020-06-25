using System.ComponentModel.DataAnnotations;

namespace DefenseByNight.API.Data.Entities.GameEntities
{
    public class Archetype
    {
        [Key]
        public string ArchetypeKey { get; set; }

        [Required]
        public string Description { get; set; }
    }
}