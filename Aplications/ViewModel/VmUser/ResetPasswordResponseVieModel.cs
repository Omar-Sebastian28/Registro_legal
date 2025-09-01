using System.ComponentModel.DataAnnotations;

namespace RegistroLegal.Core.Aplications.ViewModel.VmUser
{
    public class ResetPasswordResponseVieModel
    {
        public string? Origin { get; set; }

        [Required(ErrorMessage = "Por favor, escribe el nombre de usuario que estás buscando.")]
        public required string UserName { get; set; }
    }
}
