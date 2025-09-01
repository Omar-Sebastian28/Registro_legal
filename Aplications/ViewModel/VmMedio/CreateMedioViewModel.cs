using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RegistroLegal.Core.Aplications.ViewModel.VmMedio
{
    public class CreateMedioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe especificar el nombre del medio.")]
        public required string NombreMedio { get; set; }

        public required string Descripcion { get; set; }

        [Required(ErrorMessage = "Especifique el link de referencia de la noticia.")]
        public string? LinkReferencia { get; set; }


        [DataType(DataType.Upload)]

        //Para saber que usuario lo creo.
        public string? CreatedById { get; set; }

        public IFormFile? Foto { get; set; }
    }
}
