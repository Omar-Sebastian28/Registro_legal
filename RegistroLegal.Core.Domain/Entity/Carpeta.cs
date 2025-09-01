namespace RegistroLegal.Core.Domain.Entity
{
    public class Carpeta
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

        //Para saber que usuario lo creo.
        public string? CreatedById { get; set; }

        public ICollection<InfraccionCarpeta>? CarpetaInfracciones { get; set; }
    }
}
