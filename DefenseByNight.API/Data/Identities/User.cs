using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DefenseByNight.API.Data.Identities
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastActive { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}