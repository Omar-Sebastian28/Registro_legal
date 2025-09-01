using RegistroLegal.Core.Aplications.ViewModel.VmMedio;
using RegistroLegal.Core.Aplications.ViewModel.VmPersona;

namespace RegistroLegal.Core.Aplications.ViewModel.VmIlicito
{
    public class BasicIlicitoViewModel
    {
        public int Id { get; set; }

        public required string Tipo { get; set; }

        public required string Descripcion { get; set; }

        public DateTime FechaAcusacion { get; set; }

        public DateTime? FechaFin { get; set; }

        //Para saber que usuario lo creo.
        public string? CreatedById { get; set; }

        // Relación: cada cargo pertenece a una persona
        public required int PersonaId { get; set; }
        public PersonaViewModelBasic? Persona { get; set; }


        // Relación: cada cargo fue reportado por un medio
        public int? MedioId { get; set; }
        public BasicMedioViewModel? Medio { get; set; }
    }
}
