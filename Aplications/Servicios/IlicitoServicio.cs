using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RegistroLegal.Core.Aplications.Dto.DtoMedio;
using RegistroLegal.Core.Aplications.Dto.IlicitoDto;
using RegistroLegal.Core.Aplications.Dto.PersonaDto;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Domain.Entity;
using RegistroLegal.Core.Domain.Interfaces;
namespace Aplications.Servicios
{
    public class IlicitoServicio : GenericServices<Ilicito, DtoBasicIlicito> ,IIlicitoServicio
    {
        private readonly IIlicitoRepoitorio _ilicitoRepoitorio;
        private readonly IMapper _mapper;

        public IlicitoServicio(IIlicitoRepoitorio ilicitoRepoitorio, IMapper mapper) : base(ilicitoRepoitorio, mapper)
        {
            _ilicitoRepoitorio = ilicitoRepoitorio;
            _mapper = mapper;
        }


        public override async Task<bool> UpdateAsync(DtoBasicIlicito dto, int id)
        {
            var entityDb = await _ilicitoRepoitorio.GetByIdAsync(id);
            if (entityDb != null)
            {
                return false;
            }

            DtoBasicIlicito dtoIlicito = new()
            {
                Id = dto.Id,
                Descripcion = dto.Descripcion,
                Tipo = dto.Tipo,
                FechaAcusacion = dto.FechaAcusacion,
                FechaFin = dto.FechaFin,
                MedioId = dto.MedioId,
                PersonaId = dto.PersonaId,
                CreatedById = entityDb?.CreatedById
            };
            await base.UpdateAsync(dtoIlicito, dtoIlicito.Id);
            return true;
        }


        public async Task<List<DtoBasicIlicito>?> GetWithInclude(string? userId = null)
        {
            var listQuery = _ilicitoRepoitorio.GetQueryable()
                    .Include(ilicito => ilicito.Persona)
                    .Include(ilicito => ilicito.Medio).Where(i => i.CreatedById == userId);

            if (listQuery is not null)
            {
                try
                {
                    var listDto = listQuery.Select(i => new DtoBasicIlicito()
                    {
                        Id = i.Id,
                        Descripcion = i.Descripcion,
                        Tipo = i.Tipo,
                        CreatedById = i.CreatedById,
                        Persona = i.Persona == null ? null : new DtoPersona()
                        {
                            Id = i.Persona.Id,
                            NombrePersona = i.Persona.NombrePersona,
                            Apellido = i.Persona.Apellido,
                            Nacionalidad = i.Persona.Nacionalidad,
                            Cedula = i.Persona.Cedula
                        },
                        PersonaId = i.PersonaId,
                        FechaAcusacion = i.FechaAcusacion,
                        FechaFin = i.FechaFin,
                        MedioId = i.MedioId,
                        Medio = i.Medio != null
                        ? new MedioDto()
                        {
                            Id = i.Medio.Id,
                            NombreMedio = i.Medio.NombreMedio,
                            Descripcion = i.Medio.Descripcion,
                            Foto = i.Medio.Foto,
                            LinkReferencia = i.Medio.LinkReferencia,
                        } : null,
                    });

                    return await listDto.ToListAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error tratando de obtener la lista de ilicitos. {ex.Message}");
                    return [];
                }
            }
            return [];
        }
    }
}
