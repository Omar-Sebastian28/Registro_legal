using RegistroLegal.Core.Aplications.Dto.Email;

namespace RegistroLegal.Core.Aplications.Interfaces
{
    public interface IEmailServices
    {
        Task SendAsync(EmailRequestDto dto);
    }
}