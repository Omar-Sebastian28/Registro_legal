using Aplications.Servicios;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RegistroLegal.Core.Aplications.Dto.DtoMedio;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Domain.Entity;
using RegistroLegal.Core.Domain.Interfaces;

namespace RegistroLegal.Core.Aplications.Servicios
{
    public class MedioServicio : GenericServices<Medio,MedioDto >, IMedioServicio
    {
        private readonly IMedioRepositorio _medioRepositorio;
        private readonly IMapper _mapper;

        public MedioServicio(IMedioRepositorio medioRepositorio, IMapper mapper) : base(medioRepositorio, mapper)
        {
            _medioRepositorio = medioRepositorio;
            _mapper = mapper;
        }

        public override async Task<bool> UpdateAsync(MedioDto dto, int id)
        {
            var entityBd = await _medioRepositorio.GetByIdAsync(id);
            if (entityBd == null)
            {
                return false;
            }

            MedioDto dtoMedio = new()
            {
                Id = dto.Id,
                NombreMedio = dto.NombreMedio,
                LinkReferencia = dto.LinkReferencia,
                Descripcion = dto.Descripcion,
                CreatedById = entityBd?.CreatedById,
                Foto = string.IsNullOrEmpty(dto.Foto) ? entityBd?.Foto : dto.Foto,
            };

            await base.UpdateAsync(dtoMedio, dtoMedio.Id);
            return true;
        }

        public async Task<List<MedioDto>> GetAllWithInclude(string? userId)
        {
            var listEntity = _medioRepositorio.GetAllQueryWithInclude(["Cargos"]).Where(m => m.CreatedById == userId);
            if (!listEntity.Any()) 
            {
                return [];
            }
            try
            {
                var listDto = listEntity.ProjectTo<MedioDto>(_mapper.ConfigurationProvider);
                return await listDto.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar la lista de medios: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Detalle interno: {ex.InnerException.Message}");
                }
            }
            return [];
        }
    }
}
