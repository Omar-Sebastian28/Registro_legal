using Aplications.Servicios;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RegistroLegal.Core.Aplications.Dto.DtoCarpeta;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Domain.Entity;
using RegistroLegal.Core.Domain.Interfaces;

namespace RegistroLegal.Core.Aplications.Servicios
{
    public class CarpetaServicio : GenericServices<Carpeta, CarpetasDto>, ICarpetaServicio
    {
        private readonly ICarpetaRepositorio _carpetaRepositorio;
        private readonly IMapper _mapper;

        public CarpetaServicio(ICarpetaRepositorio carpetaRepositorio, IMapper mapper) : base(carpetaRepositorio, mapper)
        {
            _carpetaRepositorio = carpetaRepositorio;
            _mapper = mapper;
        }

        public async Task<List<CarpetasDto>?> GetWithInclude(string? userId = null)
        {
            var QueryBd = _carpetaRepositorio.GetAllQueryWithInclude(["CarpetaInfracciones"]).Where(c => c.CreatedById == userId);
            if (!QueryBd.Any()) 
            {
                return [];
            }
            try
            {
               var listDto = QueryBd.ProjectTo<CarpetasDto>(_mapper.ConfigurationProvider);
               return await listDto.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar la lista de las carpetas.{ex.Message}");
                return [];
            }
        }
    }

}

