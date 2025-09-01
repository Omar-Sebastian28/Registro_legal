using RegistroLegal.Core.Domain.Entity;

namespace RegistroLegal.Core.Domain.Interfaces
{
    public interface IUserRepositorio : IGenericoRepoitorio<User>
    {
       Task<User?> LoginAsync(string email, string password);
    }
}
