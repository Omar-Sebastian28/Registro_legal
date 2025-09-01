using RegistroLegal.Core.Aplications.Dto.UserDto;

namespace RegistroLegal.Core.Aplications.Interfaces
{
    public interface IAccountServicesForWebApp
    {
        Task<LoginReponseDto> AuthenticateAsync(LoginDto loginDto);
        Task<DtoUser?> BuscarUsuarioPorEmail(string email);
        Task<DtoUser?> BuscarUsuarioPorUserName(string userName);
        Task<string> ConfirmAccount(string userId, string token);
        Task<ResetPasswordResponseDto> ConfirmForgotPassword(ResetPasswordRequestDto dto);
        Task<DeleteResponseDto> DeleteAsync(string userId);
        Task<ResponseDto> EditUser(SaveUserDto saveUserDto, string origin, bool? creando = false);
        Task<ResetPasswordResponseDto> ForgotPassword(ResetPasswordResponseDto dto);
        Task<List<DtoUser>> GetAllUser(bool? isActive = true);
        Task<ResponseDto> RegisterAsync(SaveUserDto saveUserDto, string origin);
        Task<DtoUser?> BuscarUsuarioPorId(string Id);
        Task SingOutAsync();
    }
}