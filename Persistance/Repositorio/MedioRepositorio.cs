using RegistroLegal.Core.Domain.Entity;
using RegistroLegal.Core.Domain.Interfaces;
using RegistroLegal.Infraestructura.Persistance.Context;

namespace RegistroLegal.Infraestructura.Persistance.Repositorio
{
    public class MedioRepositorio : GenericoRepoitorio<Medio>, IMedioRepositorio
    {
        private readonly AppRegistroLegalContext _context;

        public MedioRepositorio(AppRegistroLegalContext context) : base(context)
        {
            this._context = context;
        }

   }
}
