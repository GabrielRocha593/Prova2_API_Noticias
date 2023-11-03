using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UsuariosAPI.Data;
using Xunit;

namespace UsuarioAPI.Login.testes.Fectories;

public class NoticiasApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly DotNet.Testcontainers.Containers.IContainer _mySqlContainer = new ContainerBuilder()
        .WithImage("mysql:8.0")
        .WithEnvironment("MYSQL_ROOT_PASSWORD", "weakPassword123")
        .WithPortBinding(8081, 3306)
        .Build();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
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
            services.AddDbContext<UsuarioDbContext>(options =>
                options.UseMySql(connString, ServerVersion.AutoDetect(connString)));

        });
    }
    public async Task InitializeAsync()
    {
        await _mySqlContainer.StartAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _mySqlContainer.StopAsync();
    }
}
