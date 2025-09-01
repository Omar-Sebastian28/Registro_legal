namespace RegistroLegal.Core.Aplications.Dto.UserDto
{
    public class ResponseDto
    {
        public required string Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }

        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public string? ImagenPerfil { get; set; }
        public List<string>? Roles { get; set; }


        public string? Rol { get; set; }
        public string? Error { get; set; }

        public bool HasError { get; set; }
    }
}
