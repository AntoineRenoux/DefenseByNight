using System;
using System.ComponentModel.DataAnnotations;

namespace DefenseByNight.API.Data.Entities
{
    public class Photo
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string PublicId { get; set; }
        
        public DateTime DateAdded { get; set; }
    }
}