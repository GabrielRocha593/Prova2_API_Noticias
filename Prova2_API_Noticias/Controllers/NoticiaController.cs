using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prova2_API_Noticias.Data;
using Prova2_API_Noticias.Data.Dtos.Noticia;
using Prova2_API_Noticias.Models;
using Prova2_API_Noticias.Services;
using System.Collections.Generic;

namespace Prova2_API_Noticias.Controllers
{

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
        public IActionResult AdicionaNoticia([FromBody] CreateNoticiaDto NoticiaDto)
        {


            ReadNoticiaDto readDto = _NoticiaService.AdicionaNoticia(NoticiaDto);            

            return CreatedAtAction(nameof(RecuperaNoticiaPorId), new { Id = readDto.Id }, readDto);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaNoticiaPorId(int id)
        {
            ReadNoticiaDto readDto =  _NoticiaService.RecuperaNoticiaPorId(id);
            if (readDto != null) { return Ok(readDto); };
            return NotFound();
        }

        [HttpGet]
        public IEnumerable<ReadNoticiaDto> RecuperaNoticia([FromQuery] int? noticiaId = null)
        {
            List <ReadNoticiaDto> readDto = _NoticiaService.RecuperaNoticia(noticiaId);

            return readDto;
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaNoticia(int id, [FromBody] UpdateNoticiaDto noticiaDto)
        {
            Result resultado = _NoticiaService.AtualizaNoticia(id, noticiaDto);
            if (resultado.IsFailed){ return NotFound(); }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaNoticia(int id)
        {
            Result resultado = _NoticiaService.DeletaNoticia(id);

            if (resultado.IsFailed) { return NotFound(); }
            return NoContent();
        }
    }
}
