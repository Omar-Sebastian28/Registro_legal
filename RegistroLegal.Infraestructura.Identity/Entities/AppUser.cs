using Microsoft.AspNetCore.Identity;
using RegistroLegal.Core.Domain.Entity;

namespace RegistroLegal.Infraestructura.Identity.Entities
{
    public class AppUser : IdentityUser
    {
        public required string Nombre { get; set; }

        public required string Apellido { get; set; }

        public string? ImagenPerfil { get; set; }
    }
}
