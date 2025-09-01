using System.ComponentModel.DataAnnotations;

namespace RegistroLegal.Core.Aplications.ViewModel.VmCarpeta
{
    public class CreateCarpetaViewModel
    {
        public int Id { get; set; }

        public string? CreatedById { get; set; }

        [Required (ErrorMessage = "Debe especificar el nombre de la carpeta.")]
        public required string Nombre { get; set; }        
    }
}
