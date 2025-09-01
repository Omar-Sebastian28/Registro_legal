using System.Diagnostics.CodeAnalysis;

namespace RegistroLegal.Core.Aplications.Dto.UserDto
{
    public class LoginDto
    {
        public required string UserName { get; set; }
        
        public required string Password { get; set; }
    }
}
