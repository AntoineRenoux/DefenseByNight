using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DefenseByNight.API.Data.Entities
{
    public class Discipline
    {
        [Key]
        public string DisciplineKey { get; set; }

        [Required]
        public string DisciplineName { get; set; }

        [Required]
        public string Description { get; set; }

        public string TestScore { get; set; }

        public ICollection<Power> Powers { get; set; }

    }
}