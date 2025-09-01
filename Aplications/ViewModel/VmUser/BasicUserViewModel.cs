namespace RegistroLegal.Core.Aplications.ViewModel.VmUser
{
    public class BasicUserViewModel
    {
        public required string Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public string? ImagenPerfil { get; set; }
        public required string Role { get; set; }
    }
}
