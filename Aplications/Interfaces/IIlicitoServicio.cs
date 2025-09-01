using RegistroLegal.Core.Aplications.Dto.IlicitoDto;

namespace RegistroLegal.Core.Aplications.Interfaces
{
    public interface IIlicitoServicio : IGenericServices<DtoBasicIlicito> 
    {
        Task<List<DtoBasicIlicito>?> GetWithInclude(string? userId = null);   
    }
}