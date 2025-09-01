using RegistroLegal.Core.Aplications.Dto.PersonaDto;

namespace RegistroLegal.Core.Aplications.Interfaces
{
    public interface IPersonaServicio : IGenericServices<DtoPersona>
    {
        Task<List<DtoPersona>> GetAllWithInclude(string? userId = null, string ? nombrePersona = null, string? cedula = null);      
    }
}