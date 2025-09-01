namespace RegistroLegal.Core.Domain.Entity;

public class Ilicito
{
    public int Id { get; set; }

    public required string Tipo { get; set; } //Robo, Fraude, Homicidio, etc.

    public required string Descripcion { get; set; }

    public DateTime FechaAcusacion { get; set; }

    public DateTime? FechaFin { get; set; }

    // Relación: cada cargo pertenece a una persona
    public int PersonaId { get; set; }
    public Persona? Persona { get; set; }

    //Para saber que usuario lo creo.
    public string? CreatedById { get; set; }

    // Relación: cada cargo fue reportado por un medio
    public int? MedioId { get; set; }
    public Medio? Medio { get; set; }


    //Relacion de cada infraccion por carpeta.
    public ICollection<InfraccionCarpeta>? CarpetaInfracciones { get; set; }
}
