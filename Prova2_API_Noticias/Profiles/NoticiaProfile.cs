using AutoMapper;
using Prova2_API_Noticias.Data.Dtos.Noticia;
using Prova2_API_Noticias.Models;

namespace Prova2_API_Noticias.Profiles
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
