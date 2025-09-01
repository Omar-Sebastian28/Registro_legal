using AutoMapper;
using RegistroLegal.Core.Aplications.Dto.DtoCarpeta;
using RegistroLegal.Core.Aplications.ViewModel.VmCarpeta;

namespace RegistroLegal.Core.Aplications.Mapeos.DtoToViewModel
{
    public class CarpetaMappingProfileViewModel : Profile
    {
        public CarpetaMappingProfileViewModel()
        {
            CreateMap<CarpetasDto, BasicCarpetaViewModel>().ReverseMap();

            CreateMap<CarpetasDto, CreateCarpetaViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Elementos, opt=> opt.Ignore());

            CreateMap<CarpetasDto, DeleteCarpetaViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Elementos, opt => opt.Ignore());
        }
    }
}
