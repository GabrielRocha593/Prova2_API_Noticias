using Azure;
using System.Net;
using System.Net.Http.Json;
using UsuarioAPI.Login.testes.Fectories;
using UsuariosAPI.Data.Dtos;
using Xunit;

namespace UsuarioAPI.Login.testes.Controllers;

public class UsuarioControllerTests: IClassFixture<UsuarioApiFactory>
{
    private readonly UsuarioApiFactory _factory;

    public UsuarioControllerTests(UsuarioApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CriaUsuarioFeliz()
    {
        //Arrange
        var client = _factory.CreateClient();
        //Act
        var request = new CreateUsuarioDto
        {
            Username = "Gabriel3",
            Email = "gabriel_r_salomao@hotmail.com",
            Password = "Salomao@61097",
            Repassword = "Salomao@61097",
            tipo = 0
        };

        Thread.Sleep(20000);
        var response = await client.PostAsJsonAsync("Usuario/cadastro", request);
        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CriaUsuarioSemSenha()
    {
        //Arrange
        var client = _factory.CreateClient();
        //Act
        var request = new CreateUsuarioDto
        {
            Username = "Gabriel3",
            Email = "gabriel_r_salomao@hotmail.com",
            Password = "",
            Repassword = "Salomao@61097",
            tipo = 0
        };

        Thread.Sleep(20000);
        var response = await client.PostAsJsonAsync("Usuario/cadastro", request);
        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CriaUsuarioSemReSenha()
    {
        //Arrange
        var client = _factory.CreateClient();
        //Act
        var request = new CreateUsuarioDto
        {
            Username = "Gabriel3",
            Email = "gabriel_r_salomao@hotmail.com",
            Password = "Salomao@61097",
            Repassword = "",
            tipo = 0
        };

        Thread.Sleep(20000);
        var response = await client.PostAsJsonAsync("Usuario/cadastro", request);
        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CriaUsuarioSemEmail()
    {
        //Arrange
        var client = _factory.CreateClient();
        //Act
        var request = new CreateUsuarioDto
        {
            Username = "Gabriel3",
            Email = "",
            Password = "Salomao@61097",
            Repassword = "Salomao@61097",
            tipo = 0
        };

        Thread.Sleep(20000);
        var response = await client.PostAsJsonAsync("Usuario/cadastro", request);
        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CriaUsuarioSemUsuario()
    {
        //Arrange
        var client = _factory.CreateClient();
        //Act
        var request = new CreateUsuarioDto
        {
            Username = "",
            Email = "gabriel_r_salomao@hotmail.com",
            Password = "Salomao@61097",
            Repassword = "Salomao@61097",
            tipo = 0
        };

        Thread.Sleep(20000);
        var response = await client.PostAsJsonAsync("Usuario/cadastro", request);
        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Loginfeliz()
    {
        //Arrange
        var client = _factory.CreateClient();
        //Act
        var request = new CreateUsuarioDto
        {
            Username = "Gabriel3",
            Email = "gabriel_r_salomao@hotmail.com",
            Password = "Salomao@61097",
            Repassword = "Salomao@61097",
            tipo = 0
        };

        Thread.Sleep(20000);
        var responseUsuario = await client.PostAsJsonAsync("Usuario/cadastro", request);

        if (Equals(HttpStatusCode.OK, responseUsuario.StatusCode))
        {
            var requestLogin = new LoginUsuarioDto
            {
                Username = "Gabriel3",
                Password = "Salomao@61097"
            };

           var responseLogin = await client.PostAsJsonAsync("Usuario/login", requestLogin);

            //Assert
            Assert.NotNull(responseLogin);
            Assert.Equal(HttpStatusCode.OK, responseLogin.StatusCode);
        }        
    }

    [Fact]
    public async Task LoginComSenhaErrada()
    {
        //Arrange
        var client = _factory.CreateClient();
        //Act
        var request = new CreateUsuarioDto
        {
            Username = "Gabriel3",
            Email = "gabriel_r_salomao@hotmail.com",
            Password = "Salomao@61097",
            Repassword = "Salomao@61097",
            tipo = 0
        };

        Thread.Sleep(20000);
        var responseUsuario = await client.PostAsJsonAsync("Usuario/cadastro", request);

        if (Equals(HttpStatusCode.OK, responseUsuario.StatusCode))
        {
            var requestLogin = new LoginUsuarioDto
            {
                Username = "Gabriel3",
                Password = "Salomao@6109"
            };

            var responseLogin = await client.PostAsJsonAsync("Usuario/login", requestLogin);

            //Assert
            Assert.NotNull(responseLogin);
            Assert.Equal(HttpStatusCode.Unauthorized, responseLogin.StatusCode);
        }
    }

    [Fact]
    public async Task LoginComUsuarioErrado()
    {
        //Arrange
        var client = _factory.CreateClient();
        //Act
        var request = new CreateUsuarioDto
        {
            Username = "Gabriel3",
            Email = "gabriel_r_salomao@hotmail.com",
            Password = "Salomao@61097",
            Repassword = "Salomao@61097",
            tipo = 0
        };

        Thread.Sleep(20000);
        var responseUsuario = await client.PostAsJsonAsync("Usuario/cadastro", request);

        if (Equals(HttpStatusCode.OK, responseUsuario.StatusCode))
        {
            var requestLogin = new LoginUsuarioDto
            {
                Username = "Gabriel",
                Password = "Salomao@61097"
            };

            var responseLogin = await client.PostAsJsonAsync("Usuario/login", requestLogin);

            //Assert
            Assert.NotNull(responseLogin);
            Assert.Equal(HttpStatusCode.Unauthorized, responseLogin.StatusCode);
        }
    }
}
