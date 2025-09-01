namespace RegistroLegal.Core.Aplications.Dto.UserDto
{
    public class LoginReponseDto
    {
        public required string Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public bool IsVerified { get; set; }
        public string? Error { get; set; }
        public bool HasError { get; set; }
        public List<string>? Roles { get; set; }
        public string? ImagenPerfil { get; set; }
        public required string Role { get; set; }
    }
}
