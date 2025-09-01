using System.ComponentModel.DataAnnotations;

namespace RegistroLegal.Core.Aplications.ViewModel.VmInfraccionCarpeta
{
    public class CreateInfraccionCarpetaViewModel
    {
        public int Id { get; set; } // PK
       
        public int CarpetaId { get; set; }

        [Required(ErrorMessage = "Debes selecionar una infraccion.")]
        public int InfraccionId { get; set; }

        [Required(ErrorMessage = "Debes registrar la fecha.")]
        [DataType(DataType.Date)]
        public DateTime? FechaAgregado { get; set; }
    }
}
