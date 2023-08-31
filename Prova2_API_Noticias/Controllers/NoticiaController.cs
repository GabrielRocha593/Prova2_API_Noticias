using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prova2_API_Noticias.Data;
using Prova2_API_Noticias.Data.Dtos.Noticia;
using Prova2_API_Noticias.Models;

namespace Prova2_API_Noticias.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class NoticiaController : ControllerBase
    {
        private PrincipalContext _context;
        private IMapper _mapper;

        public NoticiaController(PrincipalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaNoticia([FromBody] CreateNoticiaDto NoticiaDto)
        {
            Noticia noticia = _mapper.Map<Noticia>(NoticiaDto);
            noticia.DataPublicacao = DateTime.Now;
            _context.Noticia.Add(noticia);
            _context.SaveChanges();

            return CreatedAtAction(nameof(RecuperaNoticiaPorId), new { Id = noticia.Id }, noticia);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaNoticiaPorId(int id)
        {
            Noticia noticia = _context.Noticia.FirstOrDefault(noticia => noticia.Id == id);
            if (noticia != null)
            {
                ReadNoticiaDto noticiaDto = _mapper.Map<ReadNoticiaDto>(noticia);
                return Ok(noticiaDto);
            }
            return NotFound();
        }

        [HttpGet]
        public IEnumerable<ReadNoticiaDto> RecuperaNoticia([FromQuery] int? noticiaId = null)
        {
            if (noticiaId == null)
            {
                return _mapper.Map<List<ReadNoticiaDto>>(_context.Noticia.ToList());
            }            
            return _mapper.Map<List<ReadNoticiaDto>>(_context.Noticia.Where(noticia => noticia.Id == noticiaId).ToList()); ;
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaNoticia(int id, [FromBody] UpdateNoticiaDto noticiaDto)
        {
            Noticia noticia = _context.Noticia.FirstOrDefault(noticia => noticia.Id == id);
            if (noticia == null)
            {
                return NotFound();
            }
            noticia.DataPublicacao = DateTime.Now;

            _mapper.Map(noticiaDto, noticia);
            _context.Update(noticia);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaNoticia(int id)
        {
            Noticia noticia = _context.Noticia.FirstOrDefault(noticia => noticia.Id == id);
            if (noticia == null)
            {
                return NotFound();
            }
            _context.Remove(noticia);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
