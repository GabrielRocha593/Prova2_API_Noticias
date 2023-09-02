using AutoMapper;
using UsuariosAPI.Models;
using UsuariosAPI.Data.Dtos.Noticia;

namespace UsuariosAPI.Profiles
{
    public class NoticiaProfile : Profile
    {
        public NoticiaProfile()
        {
            CreateMap<CreateNoticiaDto, Noticia>();
            CreateMap<Noticia, ReadNoticiaDto>();
            CreateMap<UpdateNoticiaDto, Noticia>();
        }

    }
}
