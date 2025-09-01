namespace RegistroLegal.Core.Aplications.Dto.DtoCarpeta
{
    public class CarpetasDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

        public string? CreatedById { get; set; }
        public int? Elementos { get; set; }
    }
}

