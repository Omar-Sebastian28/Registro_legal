using AutoMapper;
using RegistroLegal.Core.Aplications.Dto.IlicitoDto;
using RegistroLegal.Core.Aplications.ViewModel.VmIlicito;

namespace RegistroLegal.Core.Aplications.Mapeos.DtoToViewModel
{
    public class IlicitoMappingProfileViewModel : Profile
    {
        public IlicitoMappingProfileViewModel()
        {

            CreateMap<DtoBasicIlicito, BasicIlicitoViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Medio, opt => opt.Ignore())
                .ForMember(dest => dest.Persona, opt => opt.Ignore());


            CreateMap<DtoBasicIlicito, CreateIlicitoViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Medio, opt => opt.Ignore())
                .ForMember(dest => dest.Persona, opt => opt.Ignore());


            CreateMap<DtoBasicIlicito, UpdateIlicitoViewModel>()
              .ReverseMap()
              .ForMember(dest => dest.Medio, opt => opt.Ignore())
              .ForMember(dest => dest.Persona, opt => opt.Ignore());


            CreateMap<DtoBasicIlicito, DeleteInfraccionViewModel>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo))
             .ReverseMap()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo))          
             .ForMember(dest => dest.MedioId, opt => opt.Ignore())
             .ForMember(dest => dest.Medio, opt => opt.Ignore())
             .ForMember(dest => dest.Persona, opt => opt.Ignore())
             .ForMember(dest => dest.MedioId, opt => opt.Ignore())
             .ForMember(dest => dest.Descripcion, opt => opt.Ignore())
             .ForMember(dest => dest.FechaFin, opt => opt.Ignore())
             .ForMember(dest => dest.FechaAcusacion, opt => opt.Ignore());            
        }
    }
}
