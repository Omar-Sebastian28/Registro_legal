using RegistroLegal.Core.Domain.Entity;
using RegistroLegal.Core.Domain.Interfaces;
using RegistroLegal.Infraestructura.Persistance.Context;

namespace RegistroLegal.Infraestructura.Persistance.Repositorio
{
    public class InfraccionCarpetaRepositorio : GenericoRepoitorio<InfraccionCarpeta>, IInfraccionCarpetaRepositorio
    {
        private readonly AppRegistroLegalContext _context;

        public InfraccionCarpetaRepositorio(AppRegistroLegalContext context) : base(context) 
        {
            _context = context;
        }
 
    }
}
