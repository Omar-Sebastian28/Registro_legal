using AutoMapper;
using RegistroLegal.Core.Aplications.Dto.DtoInfraccionCarpeta;
using RegistroLegal.Core.Aplications.ViewModel.VmInfraccionCarpeta;

namespace RegistroLegal.Core.Aplications.Mapeos.DtoToViewModel
{
    public class InfraccionCarpetaMappingProfileViewModel : Profile
    {
        public InfraccionCarpetaMappingProfileViewModel()
        {
            CreateMap<InfraccionCarpetaDto, BasicInfraccionCarpetaViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Infraccion, opt=> opt.Ignore())
                .ForMember(dest => dest.Carpeta, opt=> opt.Ignore());

            CreateMap<InfraccionCarpetaDto, CreateInfraccionCarpetaViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Carpeta, opt => opt.Ignore())
                .ForMember(dest => dest.Infraccion, opt => opt.Ignore());

            CreateMap<InfraccionCarpetaDto, DeleteInfraccionCarpetaViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.CarpetaId, opt => opt.Ignore())
                .ForMember(dest => dest.InfraccionId, opt => opt.Ignore())
                .ForMember(dest => dest.Carpeta, opt => opt.Ignore())
                .ForMember(dest => dest.Infraccion, opt => opt.Ignore())
                .ForMember(dest => dest.FechaAgregado, opt => opt.Ignore());
        }
    }
}
