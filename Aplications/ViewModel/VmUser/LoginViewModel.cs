using System.ComponentModel.DataAnnotations;
namespace RegistroLegal.Core.Aplications.ViewModel.VmUser
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Especifique el nombre de usuario.")]
        [DataType(DataType.Text)]
        public required string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Ingrese la contraseña.")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$",
        //ErrorMessage = "Debe tener al menos 8 caracteres, incluyendo mayúsculas, minúsculas, números y símbolos.")]
        public required string Password { get; set; }
    }
}
