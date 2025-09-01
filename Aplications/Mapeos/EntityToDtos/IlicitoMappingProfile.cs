using System.Xml.Serialization;
using AutoMapper;
using RegistroLegal.Core.Aplications.Dto.DtoMedio;
using RegistroLegal.Core.Aplications.Dto.IlicitoDto;
using RegistroLegal.Core.Aplications.Dto.PersonaDto;
using RegistroLegal.Core.Domain.Entity;

namespace RegistroLegal.Core.Aplications.Mapeos.EntityToDtos
{
    public class IlicitoMappingProfile : Profile
    {
        public IlicitoMappingProfile()
        {
            CreateMap<Ilicito, DtoBasicIlicito>()
                .ReverseMap()
                .ForMember(dest => dest.Medio, opt => opt.Ignore())
                .ForMember(dest => dest.Persona, opt => opt.Ignore());
        }
    }
}
