using System;
using System.ComponentModel.DataAnnotations;

namespace DefenseByNight.API.Models
{
    public class UserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Zipcode { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime LastActive { get; set; }

        public PhotoViewModel Photo { get; set; }
    }
}