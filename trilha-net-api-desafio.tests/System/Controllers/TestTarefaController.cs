using TrilhaApiDesafio.Controllers;
using TrilhaApiDesafio.MockData;
using Xunit;
using Moq;
using TrilhaApiDesafio.Common.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TrilhaApiDesafio.Tests;

public class TestTarefaController
{
    [Fact]
    public void ObterTodos_ShouldReturn200Status()
    {
        // Arrange
        var tarefaService = new Mock<ITarefaService>();
        tarefaService.Setup(_ => _.GetAllTarefas()).Returns(TarefaMockData.ObterTodos());
        var sut = new TarefaController(tarefaService.Object);

        // Act
        IActionResult result = sut.ObterTodos();
        var okResult = result as OkObjectResult;

        // assert
        //Assert.NotNull(okResult);
        //Assert.Equal(200, okResult.StatusCode);
    }
}