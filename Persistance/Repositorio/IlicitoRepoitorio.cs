using Microsoft.EntityFrameworkCore;
using RegistroLegal.Core.Domain.Entity;
using RegistroLegal.Core.Domain.Interfaces;
using RegistroLegal.Infraestructura.Persistance.Context;


namespace RegistroLegal.Infraestructura.Persistance.Repositorio
{
    public class IlicitoRepoitorio : GenericoRepoitorio<Ilicito>, IIlicitoRepoitorio
    {

        private readonly AppRegistroLegalContext _context;


        public IlicitoRepoitorio(AppRegistroLegalContext context) : base(context) 
        {
            this._context = context;
        }

    }
}
