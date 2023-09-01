using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Dtos;

namespace UsuariosAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class CadastroController : ControllerBase
{

    [HttpPost]
    public IActionResult CadastraUsuario([FromBody] CreateUsuarioDto UsuarioDto)
    {
        return Ok();
    }
}
