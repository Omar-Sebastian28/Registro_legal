using Aplications.Servicios;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RegistroLegal.Core.Aplications.Dto.DtoInfraccionCarpeta;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Domain.Entity;
using RegistroLegal.Core.Domain.Interfaces;

namespace RegistroLegal.Core.Aplications.Servicios
{
    public class InfraccionCarpetaServicio : GenericServices<InfraccionCarpeta, InfraccionCarpetaDto>, IInfraccionCarpetaServicio
    {
        private readonly IInfraccionCarpetaRepositorio _infraccionCarpetaRepositorio;
        private readonly IMapper _mapper;

        public InfraccionCarpetaServicio(IInfraccionCarpetaRepositorio infraccionCarpetaRepositorio, IMapper mapper) : base(infraccionCarpetaRepositorio, mapper)
        {
            _infraccionCarpetaRepositorio = infraccionCarpetaRepositorio;
            _mapper = mapper;
        }

        public async Task<List<InfraccionCarpetaDto>> GetAllQueryWithInclude(int carpetaId, string? nombrePersona = null, string? cedula = null)
        {
            var query = _infraccionCarpetaRepositorio.GetAllQueryWithInclude(["Infraccion", "Carpeta"]).Where(c => c.CarpetaId == carpetaId);
            if (query is null)
            {
                return [];
            }
            try
            {          
                var listDto = query.ProjectTo<InfraccionCarpetaDto>(_mapper.ConfigurationProvider);

                if (!string.IsNullOrWhiteSpace(cedula))
                {
                    listDto = listDto.Where(c => c.Infraccion.Persona.Cedula.Contains(cedula.Trim()));
                }

                if (!string.IsNullOrWhiteSpace(nombrePersona))
                {
                    listDto = listDto.Where(c => c.Infraccion.Persona.NombrePersona.Contains(nombrePersona.Trim()));
                }

                return await listDto.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la informacion de las carpetas. {ex.Message}");
                return [];
            }
        }
    }
}
