using RegistroLegal.Core.Domain.Entity;

namespace RegistroLegal.Core.Aplications.Dto.DtoMedio
{
    public class MedioDto
    {
        public int Id { get; set; }

        public required string NombreMedio { get; set; }

        public required string Descripcion { get; set; }

        public string? LinkReferencia { get; set; }

        public string? Foto { get; set; }

        //Para saber que usuario lo creo.
        public string? CreatedById { get; set; }
        public int? Publicaciones { get; set; }
    }
}
