using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosAPI.Controllers;
using UsuariosAPI.Data.Dtos.Noticia;
using UsuariosAPI.Models;
using UsuariosAPI.Services;
using Xunit;

namespace UsuarioAPI.Login.testes.Controllers;

public class NoticiaControllerTests
{
    [Fact]
    public void Testa_Metodo_RecuperaNoticias()
    {
        // Arrange
        var noticiaServiceMock = new Mock<NoticiaService>();
        var noticias = new List<ReadNoticiaDto>
        {
            new ReadNoticiaDto { Id = 1, Titulo = "Noticia 1", Descricao = "Descricao 1" },
            new ReadNoticiaDto { Id = 2, Titulo = "Noticia 2", Descricao = "Descricao 2" }
        };
        noticiaServiceMock.Setup(repo => repo.RecuperaNoticia(null)).Returns(noticias);
        var controller = new NoticiaController(noticiaServiceMock.Object);

        // Act
        var result = controller.RecuperaNoticia();

        // Assert
        var model = Assert.IsAssignableFrom<IEnumerable<ReadNoticiaDto>>(result);
        Assert.Equal(noticias, model);
    }
}
