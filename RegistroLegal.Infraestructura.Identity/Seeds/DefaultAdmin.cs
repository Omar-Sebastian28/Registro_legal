using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RegistroLegal.Infraestructura.Identity.Entities;
using RegistroLegal.Infraestructura.Identity.Enums;

namespace RegistroLegal.Infraestructura.Identity.Seeds
{
    public static class DefaultAdmin
    {
        public static async Task Seeds(UserManager<AppUser> userManager) 
        {
            AppUser Admin = new()
            {
                Nombre = "Alejandro",
                Apellido = "Magno",
                Email = "omar45@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                UserName = "Omar28_"
            };

            if (await userManager.Users.AllAsync(u => u.Id != Admin.Id)) 
            {
                var entityAdmin = await userManager.Users.FirstOrDefaultAsync(u => u.Email == Admin.Email);
                if (entityAdmin is null) 
                {
                    await userManager.CreateAsync(Admin, "123Pa$$word!");
                    await userManager.AddToRoleAsync(Admin, RolesDefault.Admin.ToString());
                }         
            }
        
        }
    }
}
