using RegistroLegal.Core.Domain.Entity;

namespace RegistroLegal.Core.Aplications.ViewModel.VmPersona
{
    public class PersonaViewModelBasic
    {
        public int Id { get; set; }

        public required string NombrePersona { get; set; }

        public required string Apellido { get; set; }

        public string? Telefono { get; set; }

        public required string Cedula { get; set; }

        public required string Nacionalidad { get; set; }

        public string? FotoPersona { get; set; }

        public int? CantidadCrimenes { get; set; }
    }
}
