using Docker.DotNet.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.IO.Pipelines;
using UsuariosAPI.Controllers;
using UsuariosAPI.Data.Dtos.Noticia;
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
            new ReadNoticiaDto { Id = 1, Titulo = "Noticia 1", Descricao = "Descricao 1", Chapeu = "Chapeu 1", DataPublicacao = DateTime.Now, Autor = "Autor 1"},
            new ReadNoticiaDto { Id = 2, Titulo = "Noticia 2", Descricao = "Descricao 2", Chapeu = "Chapeu 2", DataPublicacao = DateTime.Now, Autor = "Autor 2"}
        };
        noticiaServiceMock.Setup(repo => repo.RecuperaNoticia(null)).Returns(noticias);
        var controller = new NoticiaController(noticiaServiceMock.Object);

        // Act
        var result = controller.RecuperaNoticia();

        // Assert
        var model = Assert.IsAssignableFrom<IEnumerable<ReadNoticiaDto>>(result);
        Assert.Equal(noticias, model);
    }

    [Fact]
    public void Testa_Metodo_RecuperaNoticiaPorId()
    {
        // Arrange
        var noticiaServiceMock = new Mock<NoticiaService>();
        var noticia = new ReadNoticiaDto { Id = 1, Titulo = "Noticia 1", Descricao = "Descricao 1", Chapeu = "Chapeu 1", DataPublicacao = DateTime.Now, Autor = "Autor 1" };

        noticiaServiceMock.Setup(repo => repo.RecuperaNoticiaPorId(noticia.Id)).Returns(noticia);
        var controller = new NoticiaController(noticiaServiceMock.Object);

        // Act
        var result = controller.RecuperaNoticiaPorId(noticia.Id);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<ReadNoticiaDto>(okResult.Value);
        Assert.Equal(noticia, model);
    }

    [Fact]
    public void Testa_Metodo_AdicionaNoticia()
    {
        // Arrange
        var noticiaServiceMock = new Mock<NoticiaService>();
        var readNoticiaDto = new ReadNoticiaDto { 
            Id = 1, Titulo = "Noticia 1", Descricao = "Descricao 1", Chapeu = "Chapeu 1", DataPublicacao = DateTime.Now, Autor = "Autor 1" 
        };
        var createNoticiaDto = new CreateNoticiaDto { 
            Titulo = "Noticia 1", Descricao = "Descricao 1", Chapeu = "Chapeu 1", Autor = "Autor 1" 
        };

        noticiaServiceMock.Setup(repo => repo.AdicionaNoticia(createNoticiaDto)).Returns(readNoticiaDto);
        var controller = new NoticiaController(noticiaServiceMock.Object);

        // Act
        var result = controller.AdicionaNoticia(createNoticiaDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var model = Assert.IsAssignableFrom<ReadNoticiaDto>(createdAtActionResult.Value);
        Assert.Equal(readNoticiaDto, model);
    }

    [Fact]
    public void Testa_Metodo_AtualizaNoticia()
    {
        // Arrange
        var noticiaServiceMock = new Mock<NoticiaService>();
        var updateNoticiaDto = new UpdateNoticiaDto { Titulo = "Noticia 1", Descricao = "Descricao 1", Chapeu = "Chapeu 1", Autor = "Autor 1" };

        noticiaServiceMock.Setup(repo => repo.AtualizaNoticia(1, updateNoticiaDto)).Returns(Result.Ok());
        var controller = new NoticiaController(noticiaServiceMock.Object);

        // Act
        var result = controller.AtualizaNoticia(1, updateNoticiaDto);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public void Testa_Metodo_DeletaNoticia()
    {
        // Arrange
        var noticiaServiceMock = new Mock<NoticiaService>();

        noticiaServiceMock.Setup(repo => repo.DeletaNoticia(1)).Returns(Result.Ok());
        var controller = new NoticiaController(noticiaServiceMock.Object);

        // Act
        var result = controller.DeletaNoticia(1);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
}
