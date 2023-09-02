using Microsoft.AspNetCore.Authorization;

namespace UsuariosAPI.Authorization;

public class TipoJornalista : IAuthorizationRequirement
{
    public TipoJornalista(int tipo)
    {
        this.tipo = tipo;
    }

    public int tipo { get; set; }
}
