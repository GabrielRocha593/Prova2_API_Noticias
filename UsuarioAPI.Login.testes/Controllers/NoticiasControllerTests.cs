using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UsuarioAPI.Login.testes.Fectories;
using UsuariosAPI.Data.Dtos;
using Xunit;
using UsuariosAPI.Data.Dtos.Noticia;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using Docker.DotNet.Models;

namespace UsuarioAPI.Login.testes.Controllers;

public class NoticiasControllerTests : IClassFixture<NoticiasApiFactory>
{
    private readonly NoticiasApiFactory _Noticiasfactory;

    public NoticiasControllerTests(NoticiasApiFactory noticiasfactory)
    {
        _Noticiasfactory = noticiasfactory;
    }

    [Fact]
    public async Task CriaNoticiaFeliz()
    {


    }

}
