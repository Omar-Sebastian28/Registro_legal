using Microsoft.Extensions.Options;
using RegistroLegal.Core.Aplications.Dto.DtoGoogleRecaptchaResponse;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Core.Domain.Settings;
using System.Text.Json;

namespace RegistroLegal.Core.Aplications.Helpers
{
    public class VerificacionCaptcha : IVerificacionCaptcha
    {
        private readonly RecaptchaSettings _recaptchaSettings;

        public VerificacionCaptcha(IOptions<RecaptchaSettings> recaptchaSettings)
        {
            _recaptchaSettings = recaptchaSettings.Value;
        }

        public async Task<GoogleRecaptchaResponseDto> IsCaptchaValid(string captchaResponse)
        {
            var secretKey = _recaptchaSettings.SecretKey;
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.PostAsync(
                    $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={captchaResponse}",
                    null);

                var json = await response.Content.ReadAsStringAsync();
                var googleResult = JsonSerializer.Deserialize<GoogleRecaptchaResponseDto>(json);

                return googleResult ?? new GoogleRecaptchaResponseDto()
                {
                    error_codes = new List<string>() { "Error al validar el captcha" },
                    success = false,
                };
            }
            catch (Exception ex)
            {
                return new GoogleRecaptchaResponseDto()
                {
                    error_codes = [ex.Message],
                    success = false,
                };
            }
        }
    }
}
