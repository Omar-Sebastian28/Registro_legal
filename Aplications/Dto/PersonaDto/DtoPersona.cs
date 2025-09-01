namespace RegistroLegal.Core.Aplications.Dto.PersonaDto
{
    public class DtoPersona : ResponseDto
    {
        public int Id { get; set; }

        public required string NombrePersona { get; set; }

        public required string Apellido { get; set; }

        public string? Telefono { get; set; }

        public required string Cedula { get; set; }

        public required string Nacionalidad { get; set; }

        public string? FotoPersona { get; set; }

        public int? CantidadCrimenes { get; set; }

        //Para saber que usuario lo creo.
        public string? CreatedById { get; set; }
    }
}
