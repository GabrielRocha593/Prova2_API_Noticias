using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsuariosAPI.Data;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services;

public class UsuarioService
{
    private IMapper _mapper;
    private UserManager<Usuario> _userManager;
    private SignInManager<Usuario> _signInManager;
    private TokenService _tokenService;
    private UsuarioDbContext _dbContext;

    public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService, UsuarioDbContext dbContext)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _dbContext = dbContext;
    }

    public async Task<IdentityResult> CadastraUsuario(CreateUsuarioDto dto)
    {
        Usuario usuario = _mapper.Map<Usuario>(dto);


        IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

        return resultado;
    }

    public async Task<UsuarioLogado> Login(LoginUsuarioDto dto)
    {
        var resultado = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

        var usuarioobj = new UsuarioLogado("", "", resultado.Succeeded);

        if (!resultado.Succeeded)
        {
            usuarioobj.mensagem = "Usuario o senha incorreta.";

            return usuarioobj;

        }

        var usuario = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());

        var token = _tokenService.GenerateToken(usuario);

        usuarioobj.Token = token;
        usuarioobj.mensagem = "Usuario logado";

        return usuarioobj;

    }
}
