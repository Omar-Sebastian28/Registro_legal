using Aplications.Servicios;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistroLegal.Core.Aplications.Helpers;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Aplications.Servicios;
using RegistroLegal.Core.Domain.Settings;
using System.Reflection;

namespace RegistroLegal.Core.Aplications
{
    public static class ServicesRegistration
    {
        public static void AddAplicationLayerIoc(this IServiceCollection services, IConfiguration config) 
        {
            #region "Extraigo los valores del appSetting para pasarselos a la clase MailSettings."
            services.Configure<RecaptchaSettings>(config.GetSection("RecaptchaSettings"));
            #endregion

            #region "Dependencia del AutoMapper"

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #endregion

            #region configuracion de inyeccion de dependencia.
            services.AddScoped<IIlicitoServicio, IlicitoServicio>();
            services.AddScoped<IPersonaServicio, PersonaServicio>();
            services.AddScoped<IMedioServicio, MedioServicio>();
            services.AddScoped<ICarpetaServicio, CarpetaServicio>();
            services.AddScoped<IInfraccionCarpetaServicio, InfraccionCarpetaServicio>();
            services.AddScoped<IVerificacionCaptcha, VerificacionCaptcha>();
            #endregion
        }
    }
}
