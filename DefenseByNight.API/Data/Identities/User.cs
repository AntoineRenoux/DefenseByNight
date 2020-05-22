using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DefenseByNight.API.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace DefenseByNight.API.Data.Identities
{
    public class User: IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? LastActive { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
        public Photo Photo { get; set; }
    }
}