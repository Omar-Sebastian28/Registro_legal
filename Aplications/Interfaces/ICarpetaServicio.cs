using RegistroLegal.Core.Aplications.Dto.DtoCarpeta;

namespace RegistroLegal.Core.Aplications.Interfaces
{
    public interface ICarpetaServicio : IGenericServices<CarpetasDto>
    {
        Task<List<CarpetasDto>?> GetWithInclude(string? userId = null);   
    }
}