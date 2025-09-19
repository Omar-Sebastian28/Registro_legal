using RegistroLegal.Core.Aplications.Dto.DtoGoogleRecaptchaResponse;

namespace RegistroLegal.Core.Aplications.Interfaces
{
    public interface IVerificacionCaptcha
    {
        Task<GoogleRecaptchaResponseDto> IsCaptchaValid(string captchaResponse);
    }
}