using FluentResults;
using UsuariosAPI.Data.Dtos.Noticia;

namespace UsuariosAPI.Interfaces
{
    public interface INoticiaService
    {
        ReadNoticiaDto RecuperaNoticiaPorId(int id);

        ReadNoticiaDto AdicionaNoticia(CreateNoticiaDto noticiaDto);

        List<ReadNoticiaDto> RecuperaNoticia(int? noticiaId);

        Result AtualizaNoticia(int id, UpdateNoticiaDto noticiaDto);

        Result DeletaNoticia(int id);
    }
}
