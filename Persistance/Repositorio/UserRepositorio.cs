using RegistroLegal.Core.Domain.Entity;
using RegistroLegal.Core.Domain.Interfaces;
using RegistroLegal.Infraestructura.Persistance.Context;
using RegistroLegal.Core.Aplications.Helpers;
using Microsoft.EntityFrameworkCore;

namespace RegistroLegal.Infraestructura.Persistance.Repositorio
{
    public class UserRepositorio : GenericoRepoitorio<User>, IUserRepositorio
    {
        private readonly AppRegistroLegalContext _context;

        public UserRepositorio(AppRegistroLegalContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> LoginAsync(string UserName, string password) 
        {

            string passwordEncriptada = PasswordEncryptation.ComputeSha256Hash(password);
            User? user = await _context.Set<User>().FirstOrDefaultAsync(u => u.UserName == UserName && u.Password == passwordEncriptada);
           
            return user;


        }
    }
}
