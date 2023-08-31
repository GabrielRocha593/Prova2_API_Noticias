using AutoMapper;
using FluentResults;
using Prova2_API_Noticias.Data;
using Prova2_API_Noticias.Data.Dtos.Noticia;
using Prova2_API_Noticias.Models;

namespace Prova2_API_Noticias.Services
{
    public class NoticiaService
    {
        private PrincipalContext _context;
        private IMapper _mapper;

        public NoticiaService(PrincipalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadNoticiaDto AdicionaNoticia(CreateNoticiaDto noticiaDto)
        {
            Noticia noticia = _mapper.Map<Noticia>(noticiaDto);
            noticia.DataPublicacao = DateTime.Now;
            _context.Noticia.Add(noticia);
            _context.SaveChanges();

            return _mapper.Map<ReadNoticiaDto>(noticia);
        }

        public ReadNoticiaDto RecuperaNoticiaPorId(int id)
        {
            Noticia noticia = _context.Noticia.FirstOrDefault(noticia => noticia.Id == id);
            if (noticia != null)
            {
                ReadNoticiaDto noticiaDto = _mapper.Map<ReadNoticiaDto>(noticia);
                return noticiaDto;
            }
            return null;
        }

        public List<ReadNoticiaDto> RecuperaNoticia(int? noticiaId)
        {

            if (noticiaId == null)
            {
                return _mapper.Map<List<ReadNoticiaDto>>(_context.Noticia.ToList());
            }
            return _mapper.Map<List<ReadNoticiaDto>>(_context.Noticia.Where(noticia => noticia.Id == noticiaId).ToList());
        }

        public Result AtualizaNoticia(int id, UpdateNoticiaDto noticiaDto)
        {

            Noticia noticia = _context.Noticia.FirstOrDefault(noticia => noticia.Id == id);
            if (noticia == null)
            {
                return Result.Fail("Notícia não encontrada!!");
            }
            noticia.DataPublicacao = DateTime.Now;

            
            _mapper.Map(noticiaDto, noticia);
            _context.SaveChanges();
            return Result.Ok();



        }

        public Result DeletaNoticia(int id)
        {
            Noticia noticia = _context.Noticia.FirstOrDefault(noticia => noticia.Id == id);
            if (noticia == null)
            {
                return Result.Fail("Notícia não encontrada!!");
            }
            _context.Remove(noticia);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
