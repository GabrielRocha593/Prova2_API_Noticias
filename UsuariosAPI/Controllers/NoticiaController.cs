using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Dtos.Noticia;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class NoticiaController : ControllerBase
{

    private NoticiaService _NoticiaService;

    public NoticiaController(NoticiaService noticiaService)
    {
        _NoticiaService = noticiaService;
    }

    [HttpPost]
    [Authorize(Policy = "TipoJornalista")]
    public IActionResult AdicionaNoticia([FromBody] CreateNoticiaDto NoticiaDto)
    {
        ReadNoticiaDto readDto = _NoticiaService.AdicionaNoticia(NoticiaDto);

        return CreatedAtAction(nameof(RecuperaNoticiaPorId), new { Id = readDto.Id }, readDto);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "TipoJornalista")]
    public IActionResult RecuperaNoticiaPorId(int id)
    {
        ReadNoticiaDto readDto = _NoticiaService.RecuperaNoticiaPorId(id);
        if (readDto != null) { return Ok(readDto); };
        return NotFound();
    }

    [HttpGet]
    [Authorize(Policy = "TipoJornalista")]
    public IEnumerable<ReadNoticiaDto> RecuperaNoticia([FromQuery] int? noticiaId = null)
    {
        List<ReadNoticiaDto> readDto = _NoticiaService.RecuperaNoticia(noticiaId);

        return readDto;
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "TipoJornalista")]
    public IActionResult AtualizaNoticia(int id, [FromBody] UpdateNoticiaDto noticiaDto)
    {
        Result resultado = _NoticiaService.AtualizaNoticia(id, noticiaDto);
        if (resultado.IsFailed) { return NotFound(); }
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "TipoJornalista")]
    public IActionResult DeletaNoticia(int id)
    {
        Result resultado = _NoticiaService.DeletaNoticia(id);

        if (resultado.IsFailed) { return NotFound(); }
        return NoContent();
    }
}
