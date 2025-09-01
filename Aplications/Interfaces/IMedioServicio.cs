using RegistroLegal.Core.Aplications.Dto.DtoMedio;

namespace RegistroLegal.Core.Aplications.Interfaces
{
    public interface IMedioServicio : IGenericServices<MedioDto>
    {
       Task<List<MedioDto>> GetAllWithInclude(string? userId);
    }
     
}