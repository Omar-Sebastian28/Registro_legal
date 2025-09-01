using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Domain.Settings;
using RegistroLegal.Infraestructura.Share.Services;

namespace RegistroLegal.Infraestructura.Share
{
    public static class ServicesRegistration
    {
        public static void AddShareLayerIoc(this IServiceCollection services, IConfiguration config) 
        {

            #region "Extraigo los valores del appSetting para pasarselos a la clase MailSettings."
            services.Configure<MailSettings>(config.GetSection("MailSettings"));
            #endregion

            #region "Inyeccion de dependencia."
            services.AddScoped<IEmailServices, EmailServices>();
            #endregion


        }
    }
}
