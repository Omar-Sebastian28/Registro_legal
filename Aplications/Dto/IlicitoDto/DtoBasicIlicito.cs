using RegistroLegal.Core.Aplications.Dto.DtoMedio;
using RegistroLegal.Core.Aplications.Dto.PersonaDto;

namespace RegistroLegal.Core.Aplications.Dto.IlicitoDto
{
    public class DtoBasicIlicito
    {
       
            public int Id { get; set; }

            public required string Tipo { get; set; } //Robo, Fraude, Homicidio, etc.

            public required string Descripcion { get; set; }

            public DateTime FechaAcusacion { get; set; }

            public DateTime? FechaFin { get; set; }

          //Para saber que usuario lo creo.
          public string? CreatedById { get; set; }

          // Relación: cada cargo pertenece a una persona
           public int PersonaId { get; set; }
            public DtoPersona? Persona { get; set; }


            // Relación: cada cargo fue reportado por un medio
            public int? MedioId { get; set; }
            public MedioDto? Medio { get; set; }
     
    }
}
