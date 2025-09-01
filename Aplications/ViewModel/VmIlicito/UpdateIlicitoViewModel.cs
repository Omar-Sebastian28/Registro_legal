using System.ComponentModel.DataAnnotations;

namespace RegistroLegal.Core.Aplications.ViewModel.VmIlicito
{
    public class UpdateIlicitoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La infraccion debe ser especificada.")]
        public required string Tipo { get; set; } //Robo, Fraude, Homicidio, etc.

        [Required(ErrorMessage = "La descripcion de la infraccion debe ser especificada.")]
        public required string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de acusacion debe ser especificada.")]
        public DateTime FechaAcusacion { get; set; }

        public DateTime? FechaFin { get; set; }


        // Relación: cada cargo pertenece a una persona
        public int PersonaId { get; set; }

        // Relación: cada cargo fue reportado por un medio
        public int? MedioId { get; set; }
    }
}
