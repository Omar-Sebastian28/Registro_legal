using AutoMapper;
using RegistroLegal.Core.Aplications.Dto.DtoCarpeta;
using RegistroLegal.Core.Domain.Entity;

namespace RegistroLegal.Core.Aplications.Mapeos.EntityToDtos
{
    public class CarpetaMappingProfile : Profile
    {
        public CarpetaMappingProfile()
        {
            CreateMap<Carpeta, CarpetasDto>()
              .ForMember(dest => dest.Elementos, opt=> opt.MapFrom(src=> src.CarpetaInfracciones != null ? src.CarpetaInfracciones.Count : 0))
                .ReverseMap()
                .ForMember(dest => dest.CarpetaInfracciones, opt=> opt.Ignore());          
        }
    }
}
