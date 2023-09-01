using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class CadastroController : ControllerBase
{
    private CadastroService _cadastroService;

    public CadastroController(CadastroService cadastroService)
    {
        _cadastroService = cadastroService;
    }

    [HttpPost]
    public IActionResult CadastraUsuario([FromBody] CreateUsuarioDto CreateoDto)
    {
        Result resultado = _cadastroService.CadastraUsuario(CreateoDto);
        if (resultado.IsFailed) return StatusCode(500);
        return Ok();
    }
}
