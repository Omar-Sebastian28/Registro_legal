namespace RegistroLegal.Core.Domain.Entity
{
    public class InfraccionCarpeta
    {
        public int Id { get; set; } // PK
        public int CarpetaId { get; set; }
        public Carpeta? Carpeta { get; set; }

        public int InfraccionId { get; set; }
        public Ilicito? Infraccion { get; set; }

        public DateTime FechaAgregado { get; set; }
    }
}
