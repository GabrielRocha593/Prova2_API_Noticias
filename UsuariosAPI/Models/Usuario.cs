using Microsoft.AspNetCore.Identity;

namespace UsuariosAPI.Models
{
    public class Usuario : IdentityUser
    {
        public int tipo { get; set; }
        public Usuario() : base() { }
    }
}
