using AutoMapper;
using RegistroLegal.Core.Aplications.Dto.DtoMedio;
using RegistroLegal.Core.Aplications.ViewModel.VmMedio;

namespace RegistroLegal.Core.Aplications.Mapeos.DtoToViewModel
{
    public class MedioMappingProfileViewModel : Profile
    {
        public MedioMappingProfileViewModel()
        {
            CreateMap<MedioDto, BasicMedioViewModel>().ReverseMap();


            CreateMap<MedioDto, CreateMedioViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Publicaciones, opt=> opt.Ignore());


            CreateMap<MedioDto, DeleteMedioViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.LinkReferencia, opt => opt.Ignore())
                .ForMember(dest => dest.Publicaciones, opt => opt.Ignore())
                .ForMember(dest => dest.Descripcion, opt => opt.Ignore())
                .ForMember(dest => dest.Foto, opt => opt.Ignore());
        }
    }
}
