using RegistroLegal.Core.Aplications.Dto.DtoInfraccionCarpeta;

namespace RegistroLegal.Core.Aplications.Interfaces
{
    public interface IInfraccionCarpetaServicio : IGenericServices<InfraccionCarpetaDto>
    {  
        Task<List<InfraccionCarpetaDto>> GetAllQueryWithInclude(int carpetaId,string? nombrePersona = null, string? cedula = null);
    }
}