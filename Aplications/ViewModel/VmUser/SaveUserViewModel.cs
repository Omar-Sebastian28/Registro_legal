using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RegistroLegal.Core.Aplications.ViewModel.VmUser
{
    public class SaveUserViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "El nombre del usuario debe ser especificado.")]
        [DataType(DataType.Text)]
        public required string Nombre { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "El apellido del usuario debe especificarse.")]
        public required string Apellido { get; set; }

        [Required(ErrorMessage = "El nombre de usuario debe ser especificado")]
        [DataType(DataType.Text)]
        public required string UserName { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña debe ser obligatoria.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$",
        ErrorMessage = "Debe tener al menos 8 caracteres, incluyendo mayúsculas, minúsculas, números y símbolos.")]
        public required string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "El email debe ser especificado.")]
        public required string Email { get; set; }


        [Compare(nameof(Password), ErrorMessage = "No coincide la contraseña")]
        [Required(ErrorMessage ="Debes confirmar la contraseña.")]
        [DataType(DataType.Password  )]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$",
        ErrorMessage = "Debe tener al menos 8 caracteres, incluyendo mayúsculas, minúsculas, números y símbolos.")]
        public required string ConfirmPasword { get; set; }

        [DataType(DataType.Text)]
        [Required (ErrorMessage = "Debe ingresar su telefono")]
        public required string Phone { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? ImagenPerfilFile { get; set; }

        public string? Role { get; set; }
    }
}
