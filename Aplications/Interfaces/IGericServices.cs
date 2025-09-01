namespace RegistroLegal.Core.Aplications.Interfaces
{
    public interface IGenericServices<DtoEntity> where DtoEntity : class
    {
        Task<DtoEntity?> AddAsync(DtoEntity dto);
     
        Task<DtoEntity?> GetById(int id);
        Task<bool> UpdateAsync(DtoEntity dto, int id);
        Task<bool> DeleteAsync(int id);
        Task<List<DtoEntity>?> GetAllListAsync();
    }
}