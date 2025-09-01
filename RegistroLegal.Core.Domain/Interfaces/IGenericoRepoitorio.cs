namespace RegistroLegal.Core.Domain.Interfaces
{
    public interface IGenericoRepoitorio<Entity> where Entity : class
    {
        Task<Entity> AddAsync(Entity entity);
        Task<List<Entity>> AddRangeAsync(List<Entity> entities);
        Task<bool> DeleteAsync(int id);
        Task<List<Entity>> GetAllAsync();
        Task<Entity?> GetByIdAsync(int id);
        IQueryable<Entity> GetQueryable();
        Task<Entity?> UpdateAsync(int id, Entity entity);
        IQueryable<Entity> GetAllQueryWithInclude(List<string> properties);

        Task<List<Entity>> GetAllListWithInclude(List<string> properties);
    }
}