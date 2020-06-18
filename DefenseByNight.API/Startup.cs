using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AutoMapper;
using DefenseByNight.API.Data;
using DefenseByNight.API.Data.Identities;
using DefenseByNight.API.Data.Interfaces;
using DefenseByNight.API.Data.Repository;
using DefenseByNight.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Localization;
using DefenseByNight.API.Data.Interfaces.IGameRepository;
using DefenseByNight.API.Data.Repository.GameRepository;

namespace DefenseByNight.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            IdentityBuilder builder = services.AddIdentityCore<User>();

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();

            services.AddCors();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                                    ValidateIssuer = false,
                                    ValidateAudience = false
                                };
                            });

            services.Configure<RequestLocalizationOptions>(options => {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("fr-FR")
                };
                
                options.DefaultRequestCulture = new RequestCulture("fr-FR");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.Configure<IdentityOptions>(options =>
                          {
                              // Password settings.
                              options.Password.RequireDigit = false;
                              options.Password.RequireLowercase = false;
                              options.Password.RequireNonAlphanumeric = false;
                              options.Password.RequireUppercase = false;
                              options.Password.RequiredLength = 8;
                              options.Password.RequiredUniqueChars = 0;

                              // Lockout settings.
                              options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                              options.Lockout.MaxFailedAccessAttempts = 5;
                              options.Lockout.AllowedForNewUsers = true;

                              // User settings.
                              options.User.AllowedUserNameCharacters =
                                                 "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._$*%@+ ùéàèêï";
                              options.User.RequireUniqueEmail = true;
                          });

            services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddAutoMapper(typeof(Startup));

            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));

            services.AddScoped<ITraductionRepository, TraductionRepository>();
            services.AddScoped<IDisciplineRepository, DisciplineRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IHealthRepository, HealthRepository>();
            services.AddScoped<IReferenceRepository, ReferenceRepository>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<IAffilationRepository, AffiliationRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Redirige les url http vers https
            //app.UseHttpsRedirection();

            app.UseRequestLocalization();
            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
