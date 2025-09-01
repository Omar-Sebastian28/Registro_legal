using RegistroLegal.Core.Domain.Entity;
using RegistroLegal.Core.Domain.Interfaces;
using RegistroLegal.Infraestructura.Persistance.Context;

namespace RegistroLegal.Infraestructura.Persistance.Repositorio
{
    public class CarpetaRepositorio : GenericoRepoitorio<Carpeta>, ICarpetaRepositorio
    {
        private readonly AppRegistroLegalContext _context;
        public CarpetaRepositorio(AppRegistroLegalContext context) : base(context) 
        {
            _context = context; 
        }

    }
}
