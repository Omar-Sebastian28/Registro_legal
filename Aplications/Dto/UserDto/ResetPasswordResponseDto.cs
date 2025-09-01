namespace RegistroLegal.Core.Aplications.Dto.UserDto
{
    public class ResetPasswordResponseDto
    {
        public bool HasError { get; set; }
        public string? Origin { get; set; }

        public string? Error { get; set; }

        public required string UserName { get; set; }
    }
}
