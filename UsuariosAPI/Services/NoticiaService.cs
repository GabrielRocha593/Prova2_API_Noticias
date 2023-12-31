﻿using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using UsuariosAPI.Data;
using UsuariosAPI.Data.Dtos.Noticia;
using UsuariosAPI.Interfaces;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services;

public class NoticiaService : INoticiaService
{
    private PrincipalContext _context;
    private IMapper _mapper;

    public NoticiaService()
    {
        // Construtor sem parâmetros
    }
    public NoticiaService(PrincipalContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public virtual ReadNoticiaDto AdicionaNoticia(CreateNoticiaDto noticiaDto)
    {
        Noticia noticia = _mapper.Map<Noticia>(noticiaDto);
        noticia.DataPublicacao = DateTime.Now;
        _context.Noticia.Add(noticia);
        _context.SaveChanges();

        return _mapper.Map<ReadNoticiaDto>(noticia);
    }

    public virtual ReadNoticiaDto RecuperaNoticiaPorId(int id)
    {
        Noticia noticia = _context.Noticia.FirstOrDefault(noticia => noticia.Id == id);
        if (noticia != null)
        {
            ReadNoticiaDto noticiaDto = _mapper.Map<ReadNoticiaDto>(noticia);
            return noticiaDto;
        }
        return null;
    }

    public virtual List<ReadNoticiaDto> RecuperaNoticia(int? noticiaId)
    {
        if (noticiaId == null)
        {
            return _mapper.Map<List<ReadNoticiaDto>>(_context.Noticia.ToList());
        }        
        return _mapper.Map<List<ReadNoticiaDto>>(_context.Noticia.Where(noticia => noticia.Id == noticiaId).ToList());
    }

    public virtual Result AtualizaNoticia(int id, UpdateNoticiaDto noticiaDto)
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

    public virtual Result DeletaNoticia(int id)
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
