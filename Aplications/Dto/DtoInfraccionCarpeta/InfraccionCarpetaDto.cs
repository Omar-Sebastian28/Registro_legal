using RegistroLegal.Core.Aplications.Dto.DtoCarpeta;
using RegistroLegal.Core.Aplications.Dto.IlicitoDto;

namespace RegistroLegal.Core.Aplications.Dto.DtoInfraccionCarpeta
{
    public class InfraccionCarpetaDto
    {
        public int Id { get; set; } 
        public int CarpetaId { get; set; }
        public CarpetasDto? Carpeta { get; set; }

        public int InfraccionId { get; set; }
        public DtoBasicIlicito? Infraccion { get; set; }

        public DateTime FechaAgregado { get; set; } = DateTime.Now;
    }
}
