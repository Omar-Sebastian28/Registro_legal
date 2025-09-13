using System.ComponentModel.DataAnnotations;

namespace RegistroLegal.Core.Aplications.ViewModel.VmUser
{
    public class ResetPasswordRequestViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public required string Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public required string Token { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$",
        ErrorMessage = "Debe tener al menos 8 caracteres, incluyendo mayúsculas, minúsculas, números y símbolos.")]
        public required string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "La contraseña no coinciden, por favor, verificar.")]
        public string? ConfirmPassword { get; set; }
    }
}
