using AutoMapper;
using RegistroLegal.Core.Aplications.Dto.DtoMedio;
using RegistroLegal.Core.Domain.Entity;

namespace RegistroLegal.Core.Aplications.Mapeos.EntityToDtos
{
    public class MedioMappingProfile : Profile
    {
        public MedioMappingProfile()
        {
            CreateMap<Medio, MedioDto>()
                .ForMember(dest => dest.Publicaciones, opt => opt.MapFrom(src => src.Cargos != null ? src.Cargos.Count : 0))
                .ReverseMap()
                .ForMember(dest => dest.Cargos, opt => opt.Ignore());
        }
    }
}
