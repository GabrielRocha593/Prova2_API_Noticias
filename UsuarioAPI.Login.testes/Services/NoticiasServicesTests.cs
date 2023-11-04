using Xunit;
using UsuariosAPI.Data.Dtos.Noticia;
using UsuariosAPI.Services;
using FluentResults;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using UsuariosAPI.Data;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Polly;
using MySqlConnector;

namespace UsuarioAPI.Login.testes.Services;

public class NoticiasServicesTests : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<Program> _factory;
    private IContainer _container;

    public NoticiasServicesTests(WebApplicationFactory<Program> factory)
    {
        _factory = geraFactory(factory);
    }

    public WebApplicationFactory<Program> geraFactory(WebApplicationFactory<Program> factory)
    {
        return factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var dbContextOptionsDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<PrincipalContext>));

                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(PrincipalContext));

                services.Remove(dbContextOptionsDescriptor);
                services.Remove(dbContextDescriptor);

                var connString = $"Server=localhost;UserID=root;Password=weakPassword123;Database=TesteNoticia;Port=8081;";
                services.AddDbContext<PrincipalContext>(options =>
                    options.UseMySql(connString, ServerVersion.AutoDetect(connString)));
            });
        });
    }

    [Fact]
    public void CriaNoticia()
    {
        //Arrange
        using var scope = _factory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var NoticiaService = serviceProvider.GetRequiredService<NoticiaService>();
        //Act

        var request = new CreateNoticiaDto
        {
            Titulo = "Desenvolvimento",
            Descricao = "Texto aleatorio achado na internet",
            Chapeu = "Texto aleatorio",
            Autor = "Gabriel"
        };

        //Thread.Sleep(20000);
        ReadNoticiaDto response = NoticiaService.AdicionaNoticia(request);
        //Assert
        Assert.NotNull(response);
    }

    [Fact]
    public void AtualizaNoticia()
    {
        //Arrange
        using var scope = _factory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var NoticiaService = serviceProvider.GetRequiredService<NoticiaService>();

        //Act
        var request = new CreateNoticiaDto
        {
            Titulo = "Desenvolvimento",
            Descricao = "Texto aleatorio achado na internet",
            Chapeu = "Texto aleatorio",
            Autor = "Gabriel"
        };
        //Thread.Sleep(20000);
        ReadNoticiaDto response = NoticiaService.AdicionaNoticia(request);

        UpdateNoticiaDto noticiaDto = new UpdateNoticiaDto();
        noticiaDto.Titulo = response.Titulo;
        noticiaDto.Descricao = response.Descricao;
        noticiaDto.Chapeu = response.Chapeu;
        noticiaDto.Autor = response.Autor;

        Result result = NoticiaService.AtualizaNoticia(response.Id, noticiaDto);

        //Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void DeletaNoticia()
    {
        //Arrange
        using var scope = _factory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var NoticiaService = serviceProvider.GetRequiredService<NoticiaService>();

        //Act
        var request = new CreateNoticiaDto
        {
            Titulo = "Desenvolvimento",
            Descricao = "Texto aleatorio achado na internet",
            Chapeu = "Texto aleatorio",
            Autor = "Gabriel"
        };

        //Thread.Sleep(20000);
        ReadNoticiaDto response = NoticiaService.AdicionaNoticia(request);
        Result result = NoticiaService.DeletaNoticia(response.Id);

        //Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void RecuperaNoticia()
    {
        //Arrange
        using var scope = _factory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var NoticiaService = serviceProvider.GetRequiredService<NoticiaService>();

        //Act
        var request = new CreateNoticiaDto
        {
            Titulo = "Desenvolvimento",
            Descricao = "Texto aleatorio achado na internet",
            Chapeu = "Texto aleatorio",
            Autor = "Gabriel"
        };

        //Thread.Sleep(20000);
        ReadNoticiaDto response = NoticiaService.AdicionaNoticia(request);
        List<ReadNoticiaDto> result = NoticiaService.RecuperaNoticia(null);

        //Assert
        ;
        Assert.False(result.IsNullOrEmpty());
    }

    [Fact]
    public void RecuperaNoticiaPorId()
    {
        //Arrange
        using var scope = _factory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var NoticiaService = serviceProvider.GetRequiredService<NoticiaService>();

        //Act
        var request = new CreateNoticiaDto
        {
            Titulo = "Desenvolvimento",
            Descricao = "Texto aleatorio achado na internet",
            Chapeu = "Texto aleatorio",
            Autor = "Gabriel"
        };

        //Thread.Sleep(20000);
        ReadNoticiaDto response = NoticiaService.AdicionaNoticia(request);
        ReadNoticiaDto result = NoticiaService.RecuperaNoticiaPorId(response.Id);

        //Assert       
        Assert.NotNull(result);
    }

    public async Task InitializeAsync()
    {
        _container = new ContainerBuilder()
        .WithImage("mysql:8.0")
        .WithEnvironment("MYSQL_ROOT_PASSWORD", "weakPassword123")
        .WithPortBinding(8081, 3306)
        .Build();

        await _container.StartAsync();

        var policy = Policy.Handle<Exception>().WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(5));

        await policy.ExecuteAsync(async () =>
        {
            using (var connection = new MySqlConnection("Server=localhost;Port=8081;User=root;Password=weakPassword123;"))
            {
                await connection.OpenAsync();
            }
        });
    }

    public async Task DisposeAsync()
    {
        await _container.StopAsync();
    }
}
