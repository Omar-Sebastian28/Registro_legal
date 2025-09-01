using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RegistroLegal.Core.Aplications.ViewModel.VmUser
{
    public class UpdateUserViewModel
    {
        public required string Id { get; set; }

        [Required(ErrorMessage = "El nombre del usuario debe ser especificado.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido del usuario debe especificarse.")]
        public required string Apellido { get; set; }

        [Required (ErrorMessage ="Debe especificar su nombre de usuario")]
        public required string UserName { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "La contraseña no es la misma.")]
        public string? ConfrimPassword { get; set; }   

        [Required(ErrorMessage = "El apellido del usuario debe especificarse.")]
        public required string Email { get; set; }
        public string? Phone { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? ImagenPerfilFile { get; set; }

        public string? Role { get; set; }
    }
}
