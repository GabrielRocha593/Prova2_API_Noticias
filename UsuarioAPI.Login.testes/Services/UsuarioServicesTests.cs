using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Services;
using Xunit;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using DotNet.Testcontainers.Builders;
using Microsoft.EntityFrameworkCore;
using UsuariosAPI.Data;
using Microsoft.Extensions.DependencyInjection;
using DotNet.Testcontainers.Containers;
using System.Security.Policy;
using MySqlConnector;
using Polly;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using MySqlX.XDevAPI;

namespace UsuarioAPI.Login.testes.Services;

public class UsuarioServicesTests : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<Program> _factory;
    private IContainer _container;

    public UsuarioServicesTests(WebApplicationFactory<Program> factory)
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
                    typeof(DbContextOptions<UsuarioDbContext>));

                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(UsuarioDbContext));

                services.Remove(dbContextOptionsDescriptor);
                services.Remove(dbContextDescriptor);

                var connString = $"Server=localhost;UserID=root;Password=weakPassword123;Database=TesteUsuario;Port=8080;";
                services.AddDbContext<UsuarioDbContext>(options =>
                    options.UseMySql(connString, ServerVersion.AutoDetect(connString)));

                var httpContext = new DefaultHttpContext();
                httpContext.Request.Host = new HostString("localhost");

                services.AddSingleton<IHttpContextAccessor>(
                    new HttpContextAccessor { HttpContext = httpContext });
            });
        });
    }

    [Fact]
    public async Task CriaUsuario()
    {
        //Arrange
        using var scope = _factory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var UsuarioService = serviceProvider.GetRequiredService<UsuarioService>();
        //Act
        var request = new CreateUsuarioDto
        {
            Username = "Gabriel3",
            Email = "gabriel_r_salomao@hotmail.com",
            Password = "Salomao@61097",
            Repassword = "Salomao@61097",
            tipo = 0
        };
        IdentityResult resultado = await UsuarioService.CadastraUsuario(request);

        //Assert
        Assert.True(resultado.Succeeded);
    }

    public async Task InitializeAsync()
    {
        _container = new ContainerBuilder()
        .WithImage("mysql:8.0")
        .WithEnvironment("MYSQL_ROOT_PASSWORD", "weakPassword123")
        .WithPortBinding(8080, 3306)
        .Build();

        await _container.StartAsync();

        var policy = Policy.Handle<Exception>().WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(5));

        await policy.ExecuteAsync(async () =>
        {
            using (var connection = new MySqlConnection("Server=localhost;Port=8080;User=root;Password=weakPassword123;"))
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
