using System.Reflection;
using Aplications.Servicios;
using Microsoft.Extensions.DependencyInjection;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Aplications.Servicios;

namespace RegistroLegal.Core.Aplications
{
    public static class ServicesRegistration
    {
        public static void AddAplicationLayerIoc(this IServiceCollection services) 
        {

            #region "Dependencia del AutoMapper"

             services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #endregion

            #region configuracion de inyeccion de dependencia.
            services.AddScoped<IIlicitoServicio, IlicitoServicio>();
            services.AddScoped<IPersonaServicio, PersonaServicio>();
            services.AddScoped<IMedioServicio, MedioServicio>();
            services.AddScoped<ICarpetaServicio, CarpetaServicio>();
            services.AddScoped<IInfraccionCarpetaServicio, InfraccionCarpetaServicio>();
            #endregion
        }
    }
}
