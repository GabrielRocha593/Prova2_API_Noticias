﻿using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{
    private UsuarioService _usuarioService;

    public UsuarioController(UsuarioService cadastroService)
    {
        _usuarioService = cadastroService;
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto){
        var resultado = await _usuarioService.CadastraUsuario(dto);
        if (resultado.Succeeded)
        {
            return Ok("Usuário cadastrado!");
        }
        return BadRequest(resultado.Errors);

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUsuarioDto dto){
        
        var UsuarioLogado = await _usuarioService.Login(dto);
        if (!UsuarioLogado.sucesso)
        {
            return Unauthorized(UsuarioLogado.mensagem);
        }
        
        return Ok(UsuarioLogado.Token);
    }
}
