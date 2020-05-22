using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DefenseByNight.API.Models
{
    public class PhotoForCreationViewModel
    {
        [Required]
        public string Url { get; set; }

        [Required]
        public IFormFile File { get; set; }

        public string PublicId { get; set; }

        public DateTime DateAdded { get; set; }

        public PhotoForCreationViewModel()
        {
            this.DateAdded = DateTime.Now;
        }
    }
}