using Microsoft.AspNetCore.Identity;

namespace DefenseByNight.API.Data.Identities
{
    public class UserRole: IdentityUserRole<string>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}