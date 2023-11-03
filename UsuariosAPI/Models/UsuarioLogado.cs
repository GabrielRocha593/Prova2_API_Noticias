using FluentResults;

namespace UsuariosAPI.Models;

public class UsuarioLogado
{
    public string? Token { get; set; }
    public string mensagem { get; set; }
    public bool sucesso { get; set; }

    public UsuarioLogado(string Token, string mensagem, bool sucesso)
    {
        this.Token = Token;
        this.mensagem = mensagem;
        this.sucesso = sucesso;
    }
}
