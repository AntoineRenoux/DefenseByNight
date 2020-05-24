using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DefenseByNight.API.Data.Entities
{
    public class Attribute
    {
        [Key]
        public string AttributeKey { get; set; }

        [Required]
        public string AttributeName { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<Focus> Focus { get; set; }
    }
}