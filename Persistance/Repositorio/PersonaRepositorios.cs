using Microsoft.EntityFrameworkCore;
using RegistroLegal.Core.Domain.Entity;
using RegistroLegal.Core.Domain.Interfaces;
using RegistroLegal.Infraestructura.Persistance.Context;

namespace RegistroLegal.Infraestructura.Persistance.Repositorio
{
    public class PersonaRepositorios : GenericoRepoitorio<Persona>, IPersonaRepositorios
    {
        private readonly AppRegistroLegalContext _context;

        public PersonaRepositorios(AppRegistroLegalContext context) : base(context)
        {
            _context = context;
        }

    }
}
