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
        public required string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "La contraseña no coinciden, por favor, verificar.")]
        public string? ConfirmPassword { get; set; }
    }
}
