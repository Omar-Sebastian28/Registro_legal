using RegistroLegal.Infraestructura.Persistance;
using RegistroLegal.Core.Aplications;
using RegistroLegal.Infraestructura.Share;
using RegistroLegal.Infraestructura.Identity;

namespace RegistroLegal
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddSession(opt => 
            {

                opt.IdleTimeout = TimeSpan.FromMinutes(60);
                opt.Cookie.HttpOnly = true;
            
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddPersistenceLayerIoc(builder.Configuration);
            builder.Services.AddLayerIdentityForWebApp(builder.Configuration);
            builder.Services.AddAplicationLayerIoc();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddShareLayerIoc(builder.Configuration);

            var app = builder.Build();

            await app.Services.AddRunSeeds();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSession(); //Le decimos que utilice las sesiones en el aplicativo.
            app.UseRouting();
           
            app.UseAuthentication(); //Primero autentica.
            app.UseAuthorization(); //Luego autoriza.

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
