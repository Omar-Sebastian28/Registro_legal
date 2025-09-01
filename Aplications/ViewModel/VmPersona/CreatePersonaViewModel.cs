using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using RegistroLegal.Core.Aplications.Helpers;

namespace RegistroLegal.Core.Aplications.ViewModel.VmPersona
{
    public class CreatePersonaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre debe ser registrado.")]
        public required string NombrePersona { get; set; }

        [Required(ErrorMessage ="El apellido debe er registrado.")]
        public required string Apellido { get; set; }

        [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Formato de teléfono inválido.")]
        [Required(ErrorMessage = "Debe regitrar el número de telefono.")]
        public required string Telefono { get; set; }

        [Required(ErrorMessage ="La cedula debe ser registrada.")]
        [CedulaDominicana]
        public required string Cedula { get; set; }

        [Required(ErrorMessage ="La nacionalidad debe ser registrada.")]
        public required string Nacionalidad { get; set; }
        public string? CreatedById { get; set; }

        public IFormFile? FotoPersona { get; set; }
    }
}
