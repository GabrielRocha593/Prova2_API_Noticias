using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace UsuariosAPI.Authorization;

public class TipoAuthorization : AuthorizationHandler<TipoJornalista>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TipoJornalista requirement)
    {
        //var claimTipo = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/tipo";
        var TipoJornalistaClain = context
        .User
        .Claims.FirstOrDefault(Claim =>
            Claim.Type == "tipo"
        );
        

        if (TipoJornalistaClain is null) return Task.CompletedTask;

        var TipoJornalista = TipoJornalistaClain.Value;

        if (Int32.Parse(TipoJornalista) == requirement.tipo) context.Succeed(requirement);

        return Task.CompletedTask;
        
    }
}
