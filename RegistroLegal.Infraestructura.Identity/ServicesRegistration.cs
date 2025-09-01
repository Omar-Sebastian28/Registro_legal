using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Infraestructura.Identity.Contexts;
using RegistroLegal.Infraestructura.Identity.Entities;
using RegistroLegal.Infraestructura.Identity.Seeds;

namespace RegistroLegal.Infraestructura.Identity
{
    public static class ServicesRegistration
    {
        public static void AddLayerIdentityForWebApp(this IServiceCollection services, IConfiguration config) 
        {
            GeneralConfiguration(services, config);

            #region Configuracion de Identity

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = true; 
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 6;

                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
            });


            services.AddIdentityCore<AppUser>()
                    .AddRoles<IdentityRole>()
                    .AddSignInManager()
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromHours(12);
            });


            //Lo más importante para que identity funcione correctamente.
            services.AddAuthentication(opt => 
            {
               opt.DefaultScheme = IdentityConstants.ApplicationScheme;
               opt.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
               opt.DefaultSignInScheme = IdentityConstants.ApplicationScheme;

            }).AddCookie(IdentityConstants.ApplicationScheme, opt=> 
            {
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(180);
                opt.LoginPath = "/Login"; 
                opt.AccessDeniedPath = "/Login/AccesoDenegado";
            });
            #endregion


            #region servicio 
            services.AddScoped<IAccountServicesForWebApp, AccountServicesForWebApp>();
            #endregion
        }


        public static async Task AddRunSeeds(this IServiceProvider service)
        {
            using var scope = service.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await DefaultRoles.SeedAsync(RoleManager);
            await DefaultAdmin.Seeds(userManager);
            await DefaultUser.Seeds(userManager);
        }


        private static void GeneralConfiguration(IServiceCollection services, IConfiguration config) 
        {
            #region Context 
            if (config.GetValue<bool>("InMemoryDb"))
            {
                services.AddDbContext<IdentityContext>(opt => opt.UseInMemoryDatabase("WebApp"));
            }
            else
            {
                var connectionStrings = config.GetConnectionString("DefaultConnection");
                services.AddDbContext<IdentityContext>(
                
                (servicesProvider, opt) =>
                {
                    opt.EnableSensitiveDataLogging();
                    opt.UseSqlServer(connectionStrings,
                     m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                },
                   contextLifetime : ServiceLifetime.Scoped,
                   optionsLifetime : ServiceLifetime.Scoped
                );             
            }
            #endregion

        }
    }
}
