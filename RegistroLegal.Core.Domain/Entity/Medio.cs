namespace RegistroLegal.Core.Domain.Entity;

public class Medio
{
    public int Id { get; set; }

    public required string NombreMedio { get; set; }

    public required string Descripcion { get; set; }

    public string? LinkReferencia { get; set; }

    public string? Foto { get; set; }

    //Para saber que usuario lo creo.
    public string? CreatedById { get; set; }


    // Relación: un medio puede reportar muchos cargos
    public ICollection<Ilicito>? Cargos { get; set; }
}
