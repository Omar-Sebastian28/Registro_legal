using AutoMapper;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Domain.Interfaces;

namespace Aplications.Servicios
{
    public class GenericServices<Entity, DtoEntity> : IGenericServices<DtoEntity>
        where DtoEntity : class 
        where Entity : class
    {
        private readonly IGenericoRepoitorio<Entity> _repositorio;
        private readonly IMapper _mapper;

        public GenericServices(IGenericoRepoitorio<Entity> repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public virtual async Task<DtoEntity?> AddAsync(DtoEntity dto)
        {
            if (dto is null)
            {
                return null;
            }
            try
            {
                Entity entity = _mapper.Map<Entity>(dto);
                await _repositorio.AddAsync(entity);

                return _mapper.Map<DtoEntity>(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear a la entidad. {ex.Message}");
                return null;
            }
        }

        public virtual async Task<DtoEntity?> GetById(int id)
        {
            try
            {
                var entity = await _repositorio.GetByIdAsync(id);

                if (entity is null)
                {
                    return null;
                }

                var entitydto = _mapper.Map<DtoEntity>(entity);
                return entitydto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar la entidad por id. {ex.Message}");
                return null;
            }
        }

        public virtual async Task<bool> UpdateAsync(DtoEntity dto, int id)
        {
            try
            {
                if (dto is null) 
                {
                    return false;
                }

                var entity = _mapper.Map<Entity>(dto);
                
                await _repositorio.UpdateAsync(id, entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la entidad. {ex.Message}");
                return false;
            }
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _repositorio.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar la entidad. {ex.Message}");
                return false;
            }
        }

        public virtual async Task<List<DtoEntity>?> GetAllListAsync() 
        {
            var entity = await _repositorio.GetAllAsync();

            if (entity is null)
            {
                return [];
            }
            try
            {
                return _mapper.Map<List<DtoEntity>>(entity);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error al traer la lista de las entidades. {ex.Message}");
                return null;
            }         
        }
      
    }
}
