using DefenseByNight.API.Data.Entities;
using DefenseByNight.API.Data.Identities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DefenseByNight.API.Data
{
    public class DataContext : IdentityDbContext<User, Role, string>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Traduction> Traductions { get; set; }
        public DbSet<Health> Health { get; set; }
        public DbSet<Reference> References { get; set; }

        #region Game
        public DbSet<Affiliate> Affiliates { get; set; }

        public DbSet<Focus> Focus { get; set; }

        public DbSet<Attribute> Attributes { get; set; }

        public DbSet<Power> Powers { get; set; }

        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<Clan> Clans { get; set; }

        public DbSet<Atout> Atouts { get; set; }

        public DbSet<Flaw> Flaws { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Traduction>()
                .HasKey(c => new { c.Key, c.LCID });

            builder.Entity<Clan>()
                .HasKey(c => c.ClanKey);
        }
    }
}