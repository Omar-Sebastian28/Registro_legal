namespace RegistroLegal.Core.Domain.Entity;


public class Persona
{
    public int Id { get; set; }

    public required string NombrePersona { get; set; }

    public required string Apellido { get; set; }

    public string? Telefono { get; set; }

    public required string Cedula { get; set; }

    public required string Nacionalidad { get; set; }

    public string? FotoPersona { get; set; }

    public ICollection<Ilicito>? Cargos { get; set;}

    //Para saber que usuario lo creo.
    public string? CreatedById { get; set; }
}
