using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services;

public class CadastroService
{
    private IMapper _mapper;
    private UserManager<IdentityUser<int>> _userManager;

    public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager)
    {
        this._mapper = mapper;
        this._userManager = userManager;
    }

    public Result CadastraUsuario(CreateUsuarioDto CreateoDto)
    {
        Usuario usuario = _mapper.Map<Usuario>(CreateoDto);
        IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
        Task<IdentityResult> resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, CreateoDto.Password);
        if (resultadoIdentity.Result.Succeeded) return Result.Ok();
        return Result.Fail("Falha ao Cadastrar Usuário");
    }
}
