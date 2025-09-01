using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RegistroLegal.Infraestructura.Identity.Entities;
using RegistroLegal.Infraestructura.Identity.Enums;

namespace RegistroLegal.Infraestructura.Identity.Seeds
{
    public static class DefaultUser
    {
        public static async Task Seeds(UserManager<AppUser> userManager) 
        {

            AppUser user = new()
            {
                Nombre = "Alejandro",
                Apellido = "Magno",
                Email = "juliocesar22@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                UserName = "JulioCesar29"
            };

            if (await userManager.Users.AllAsync(u => u.Id != user.Id)) 
            {
                var entityUser = await userManager.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (entityUser == null) 
                {
                    await userManager.CreateAsync(user, "123Pa$$word!");
                    await userManager.AddToRoleAsync(user, RolesDefault.Usuario.ToString());
                }
            }
        }
    }
}
