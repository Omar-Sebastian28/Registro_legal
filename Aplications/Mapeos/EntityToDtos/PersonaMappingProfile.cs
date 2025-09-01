using AutoMapper;
using RegistroLegal.Core.Aplications.Dto.PersonaDto;
using RegistroLegal.Core.Domain.Entity;

namespace RegistroLegal.Core.Aplications.Mapeos.EntityToDtos
{
    public class PersonaMappingProfileViewM : Profile
    {
        public PersonaMappingProfileViewM()
        {
            CreateMap<Persona, DtoPersona>()
                .ForMember(dest => dest.CantidadCrimenes, 
                           opt => opt.MapFrom(orig => orig.Cargos != null ? orig.Cargos.Count : 0))               
                .ReverseMap()
                .ForMember(dest => dest.Cargos,
                            opt => opt.Ignore());
        }
    }
}
