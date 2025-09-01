using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RegistroLegal.Core.Aplications.Dto.PersonaDto;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Domain.Entity;
using RegistroLegal.Core.Domain.Interfaces;

namespace Aplications.Servicios
{
    public class PersonaServicio : GenericServices<Persona, DtoPersona>, IPersonaServicio
    {

        private readonly IPersonaRepositorios _personaRepositorios;
        private readonly IMapper _mapper;

        public PersonaServicio(IPersonaRepositorios personaRepositorios, IMapper mapper) : base(personaRepositorios, mapper)
        {
            _personaRepositorios = personaRepositorios;
            _mapper = mapper;
        }


        public override async Task<DtoPersona?> AddAsync(DtoPersona dto)
        {
            var response = new DtoPersona()
            {
                NombrePersona = "",
                Nacionalidad = "",
                Apellido = "",
                Cedula = "",
                HasError = false,
            };

            var entityDb = _personaRepositorios.GetQueryable().Where(p => p.CreatedById == dto.CreatedById);

            var mismoNombre = await entityDb.FirstOrDefaultAsync(p => p.NombrePersona == dto.NombrePersona.Trim());
            if (mismoNombre != null) 
            {
                response.HasError = true;
                response.Error = "Ya existe un registro con este nombre. Por favor, verifica los datos ingresados.";
                return response;            
            }
          
            var mismaCedula = await entityDb.FirstOrDefaultAsync(p => p.Cedula == dto.Cedula);
            if (mismaCedula != null)
            {
                response.HasError = true;
                response.Error = "Ya existe un registro con esta cédula. Por favor, verifica los datos ingresados.";
                return response;
            }

            var mimoTelefono = await entityDb.FirstOrDefaultAsync(p => p.Telefono == dto.Telefono);
            if (mimoTelefono != null)
            {
                response.HasError = true;
                response.Error = "Este número de teléfono ya está asociado a otra persona. Verifica si hay un error de digitación.";
                return response;
            }

            var personaDto = _mapper.Map<DtoPersona>(dto);

            try
            {
               return await base.AddAsync(personaDto);
            }
            catch (Exception ex) 
            {
                response.HasError = true;
                response.Error = $"Ocurrio un error al guardar la persona. {ex}";
                return response;
            }
        }

        public override async Task<bool> UpdateAsync(DtoPersona dto, int id)
        {
            var entityBd = await _personaRepositorios.GetByIdAsync(id);
            if (entityBd == null) 
            {
                return false;
            }

            DtoPersona dtoPersona = new()
            {
                Id = dto.Id,
                NombrePersona = dto.NombrePersona,
                Apellido = dto.Apellido,
                Cedula = dto.Cedula,
                FotoPersona = string.IsNullOrEmpty(dto.FotoPersona) ? entityBd.FotoPersona : dto.FotoPersona,
                Nacionalidad = dto.Nacionalidad,
                CreatedById = entityBd?.CreatedById,
                Telefono = dto.Telefono      
            };
            await base.UpdateAsync(dtoPersona, dtoPersona.Id);
            return true;
        }
     

        public async Task<List<DtoPersona>> GetAllWithInclude(string? userId = null, string? nombrePersona = null, string? cedula = null)
        {
            var query = _personaRepositorios.GetQueryable().Where(p => p.CreatedById == userId);

            if (query == null) 
            {
                return [];
            }

            try
            {
                var lisPersonasDto =  query
                    .Select(p => new DtoPersona
                    {
                        Id = p.Id,
                        NombrePersona = p.NombrePersona,
                        Apellido = p.Apellido,
                        Cedula = p.Cedula,
                        Telefono = p.Telefono,
                        Nacionalidad = p.Nacionalidad,
                        FotoPersona = p.FotoPersona,
                        CreatedById = p.CreatedById,    
                        CantidadCrimenes = p.Cargos != null ? p.Cargos.Count : 0
                    });

                if (!string.IsNullOrWhiteSpace(cedula))
                {
                    lisPersonasDto = lisPersonasDto.Where(p => p.Cedula.Contains(cedula.Trim()));
                }

                if (!string.IsNullOrWhiteSpace(nombrePersona))
                {
                    lisPersonasDto = lisPersonasDto.Where(p => p.NombrePersona.Contains(nombrePersona.Trim()));
                }


                return await lisPersonasDto.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la lista con el include. {ex.Message}");
                if (ex.InnerException != null) 
                {
                    Console.WriteLine($"Detalle interno: {ex.InnerException.Message}");
                }
                return [];
            }
        }
    }
}
