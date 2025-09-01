namespace RegistroLegal.Core.Domain.Entity;

    public class User
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string UserName { get; set; }

        public required string Password { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public string? ImagenPerfil { get; set; }
        public required int Role { get; set; }
       
    }

