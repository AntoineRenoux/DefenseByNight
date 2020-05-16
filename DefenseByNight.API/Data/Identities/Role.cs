using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DefenseByNight.API.Data.Identities
{
    public class Role: IdentityRole
    {
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}