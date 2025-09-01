using Microsoft.EntityFrameworkCore;
using RegistroLegal.Core.Domain.Interfaces;
using RegistroLegal.Infraestructura.Persistance.Context;

namespace RegistroLegal.Infraestructura.Persistance.Repositorio
{
    public class GenericoRepoitorio<Entity> : IGenericoRepoitorio<Entity>
        where Entity : class   
    {
        private readonly AppRegistroLegalContext _context;


        public GenericoRepoitorio(AppRegistroLegalContext context)
        {
            _context = context;
        }


        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            try
            {
                await _context.Set<Entity>().AddAsync(entity);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {

                Console.WriteLine("Error al guardar la entidad en la bd." + ex.Message);

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Detalle interno: " + ex.InnerException.Message);
                }

            }
            return entity;

        }

        public virtual async Task<List<Entity>> AddRangeAsync(List<Entity> entities) 
        {
            try
            {
                await _context.Set<Entity>().AddRangeAsync(entities);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) 
            {
                Console.WriteLine($"Error al hacer una add masivo en la bd {ex.Message}");

                if (ex.InnerException != null) 
                {
                    Console.WriteLine($"Detalle interno: {ex.InnerException.Message}");
                }
            }

            return entities;
  
        }


        public virtual async Task<List<Entity>> GetAllListWithInclude(List<string> properties) 
        {
            var query = _context.Set<Entity>().AsQueryable();

            foreach (var property in properties)
            {
                query = query.Include(property);
            }
            return await query.ToListAsync();
        }


        public virtual IQueryable<Entity> GetAllQueryWithInclude(List<string> properties) 
        {
            var query = _context.Set<Entity>().AsQueryable();

            foreach (var property in properties)
            {             
                query = query.Include(property);
            }

            return query.AsQueryable();
        }                           

        public virtual async Task<Entity?> UpdateAsync(int id, Entity entity)
        {
            try
            {
                var entry = await _context.Set<Entity>().FindAsync(id);
                if (entry == null)
                {
                    return null;
                }

                _context.Entry(entry).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return entry;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Detalle interno: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Detalle interno:  {ex.InnerException.Message}");
                }
                return null;
            }
        }


        public virtual async Task<Entity?> GetByIdAsync(int id)
        {
            var entity = await _context.Set<Entity>().FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            return entity;
        }


        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<Entity>().FindAsync(id);

            if (entity is not null)
            {
                try
                {
                    _context.Set<Entity>().Remove(entity);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine($"Error al eliminar la entidad de la bd. {ex.Message}");
                    if (ex.InnerException != null) 
                    Console.WriteLine($"Detalle interno: {ex.InnerException.Message}");

                    return false;
                }
            }
            return false;
        }


        public virtual async Task<List<Entity>> GetAllAsync()
        {
            return await _context.Set<Entity>().ToListAsync();
        }


        public virtual IQueryable<Entity> GetQueryable()
        {
            return _context.Set<Entity>().AsQueryable();
        }
    }
}
