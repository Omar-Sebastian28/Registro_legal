using RegistroLegal.Core.Aplications.Dto.UserDto;

namespace RegistroLegal.Core.Aplications.Interfaces
{
    public interface IUserServicio
    {
        Task<SaveUserDto?> AddAsync(SaveUserDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<DtoUser>> GetAllList();
        Task<SaveUserDto?> GetById(int id);
        Task<bool> UpdateAsync(SaveUserDto dto);

        Task<DtoUser?> LoginAsync(LoginDto dto);
    }
}