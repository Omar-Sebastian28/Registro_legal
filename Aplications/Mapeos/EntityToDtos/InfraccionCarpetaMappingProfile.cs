using AutoMapper;
using RegistroLegal.Core.Aplications.Dto.DtoInfraccionCarpeta;
using RegistroLegal.Core.Domain.Entity;

namespace RegistroLegal.Core.Aplications.Mapeos.EntityToDtos
{
    public class InfraccionCarpetaMappingProfile : Profile
    {
        public InfraccionCarpetaMappingProfile()
        {
            CreateMap<InfraccionCarpeta, InfraccionCarpetaDto>().ReverseMap();
        }
    }
}
