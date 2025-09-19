namespace RegistroLegal.Core.Domain.Settings
{
    public class RecaptchaSettings
    {
        public required string SiteKey { get; set; }
        public required string SecretKey { get; set; }
    }
}
