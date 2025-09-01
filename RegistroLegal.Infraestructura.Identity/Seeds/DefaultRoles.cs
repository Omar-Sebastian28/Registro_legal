using Microsoft.AspNetCore.Identity;
using RegistroLegal.Infraestructura.Identity.Enums;

namespace RegistroLegal.Infraestructura.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager) 
        {
            await roleManager.CreateAsync(new IdentityRole(RolesDefault.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(RolesDefault.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(RolesDefault.Usuario.ToString()));     
        }
    }
}
