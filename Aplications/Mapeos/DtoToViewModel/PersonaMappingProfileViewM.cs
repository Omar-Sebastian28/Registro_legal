using AutoMapper;
using RegistroLegal.Core.Aplications.Dto.PersonaDto;
using RegistroLegal.Core.Aplications.ViewModel.VmPersona;

namespace RegistroLegal.Core.Aplications.Mapeos.DtoToViewModel
{
    public class PersonaMappingProfileViewM : Profile
    {
        public PersonaMappingProfileViewM()
        {
            CreateMap<DtoPersona, PersonaViewModelBasic>()
                .ReverseMap();

            CreateMap<DtoPersona, CreatePersonaViewModel>()
                .ReverseMap()
                .ForMember(dest=> dest.FotoPersona, opt => opt.MapFrom(src => src.FotoPersona));

            CreateMap<DtoPersona, DeletePersonaViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NombrePersona, opt => opt.MapFrom(src => src.NombrePersona))

                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NombrePersona, opt => opt.MapFrom(src => src.NombrePersona));                               
        }
    }
}
