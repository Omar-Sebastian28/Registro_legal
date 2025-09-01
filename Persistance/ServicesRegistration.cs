using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistroLegal.Core.Domain.Interfaces;
using RegistroLegal.Infraestructura.Persistance.Context;
using RegistroLegal.Infraestructura.Persistance.Repositorio;

namespace RegistroLegal.Infraestructura.Persistance
{
    public static class ServicesRegistration
    {
        //Extenion method. - Decorator pattern. 
        public static void AddPersistenceLayerIoc(this IServiceCollection services, IConfiguration configuration)
        {

            #region Configuracion de la bd.

            if (configuration.GetValue<bool>("InMemoryDb"))
            {
                //Bd en memoria.
                services.AddDbContext<AppRegistroLegalContext>(opt =>
                     opt.UseInMemoryDatabase("InMemoryDataBase"));
            }
            else
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                      services.AddDbContext<AppRegistroLegalContext>(opt =>
                      opt.UseSqlServer(connectionString, mg => mg.MigrationsAssembly(typeof(AppRegistroLegalContext).Assembly.FullName)), ServiceLifetime.Scoped);
            } 

            #endregion


            #region Repositorios IOC
            services.AddScoped(typeof(IGenericoRepoitorio<>), typeof(GenericoRepoitorio<>));
            services.AddScoped<IIlicitoRepoitorio, IlicitoRepoitorio>();
            services.AddScoped<IPersonaRepositorios, PersonaRepositorios>();
            services.AddScoped<IMedioRepositorio, MedioRepositorio>();
            services.AddScoped<ICarpetaRepositorio, CarpetaRepositorio>();
            services.AddScoped<IInfraccionCarpetaRepositorio, InfraccionCarpetaRepositorio>();
            #endregion
        }
    }
    
}
