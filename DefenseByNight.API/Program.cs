using System;
using DefenseByNight.API.Data;
using DefenseByNight.API.Data.Identities;
using DefenseByNight.API.Data.Seeds;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DefenseByNight.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    var userService = services.GetRequiredService<UserManager<User>>();
                    var roleService = services.GetRequiredService<RoleManager<Role>>();
                    context.Database.Migrate();
                    Seed.SeedReferences(context);
                    Seed.SeedTraductions(context);
                    Seed.SeedUsers(userService, roleService);
                    Seed.SeedAffiliate(context);
                    Seed.SeedFocus(context);
                    Seed.SeedDiscipline(context);
                    Seed.SeedClans(context);
                    Seed.SeedAttributs(context);
                    Seed.SeedAtout(context);
                    Seed.SeedFlaws(context);
                    Seed.SeedPhotos(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
