using TrilhaApiDesafio.Common.Services;
using TrilhaApiDesafio.Common.Repositories;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.MockData;
using TrilhaApiDesafio.Context;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TrilhaApiDesafio.Tests;

public class TestTarefaService : IDisposable
{
    protected readonly OrganizadorContext _context;
    public TestTarefaService()
    {
        var options = new DbContextOptionsBuilder<OrganizadorContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        _context = new OrganizadorContext(options);

        _context.Database.EnsureCreated();
    }

    [Fact]
    public Task GetAllTarefas_ReturnTarefaCollection()
    {
        /// Arrange
        _context.Tarefas.AddRange(TarefaMockData.ObterTodos());
        _context.SaveChanges();

        var tarefaRepository = new Repository<Tarefa>(_context);
        var pessoaRepository = new Repository<Pessoa>(_context);

        var sut = new TarefaService(tarefaRepository, pessoaRepository);

        /// Act
        var result = sut.GetAllTarefas();

        /// Assert
        result.Should().HaveCount(TarefaMockData.ObterTodos().Count);
        return Task.CompletedTask;
    }

    [Fact]
    public Task GetTarefaById_ReturnTarefa()
    {
        /// Arrange
        _context.Tarefas.AddRange(TarefaMockData.ObterTodos());
        _context.SaveChanges();

        var tarefaRepository = new Repository<Tarefa>(_context);
        var pessoaRepository = new Repository<Pessoa>(_context);

        var sut = new TarefaService(tarefaRepository, pessoaRepository);
        var tarefa = TarefaMockData.ObterTodos().ElementAt(0);

        /// Act
        var result = sut.GetTarefaById(tarefa.Id);

        /// Assert
        Assert.NotNull(result);
        Assert.Equal(TarefaMockData.ObterTodos().ElementAt(0).Id, result.Id);
        Assert.Equal(TarefaMockData.ObterTodos().ElementAt(0).Titulo, result.Titulo);
        return Task.CompletedTask;
    }

    [Fact]
    public Task GetTarefaById_ObjectNotExists()
    {
        /// Arrange
        _context.Tarefas.AddRange(TarefaMockData.ObterTodos());
        _context.SaveChanges();

        var tarefaRepository = new Repository<Tarefa>(_context);
        var pessoaRepository = new Repository<Pessoa>(_context);

        var sut = new TarefaService(tarefaRepository, pessoaRepository);

        /// Act
        var result = sut.GetTarefaById(99);

        /// Assert
        Assert.Null(result);
        return Task.CompletedTask;
    }

    [Fact]
    public Task CreateTarefa_SuccessOnSave()
    {
        /// Arrange
        _context.Tarefas.AddRange(TarefaMockData.ObterTodos());
        _context.SaveChanges();

        var tarefaRepository = new Repository<Tarefa>(_context);
        var pessoaRepository = new Repository<Pessoa>(_context);

        var sut = new TarefaService(tarefaRepository, pessoaRepository);

        var newTarefa = TarefaMockData.NewTarefa();

        /// Act
        var result = sut.CreateTarefa(newTarefa);

        ///Assert
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        int expectedRecordCount = (TarefaMockData.ObterTodos().Count() + 1);
        _context.Tarefas.Count().Should().Be(expectedRecordCount);

        return Task.CompletedTask;
    }

    [Fact]
    public Task CreateTarefa_ValidationFail()
    {
        /// Arrange
        _context.Tarefas.AddRange(TarefaMockData.ObterTodos());
        _context.SaveChanges();

        var tarefaRepository = new Repository<Tarefa>(_context);
        var pessoaRepository = new Repository<Pessoa>(_context);

        var sut = new TarefaService(tarefaRepository, pessoaRepository);

        var newTarefa = TarefaMockData.NewIncompleteTarefa();

        /// Act
        var result = sut.CreateTarefa(newTarefa);

        ///Assert
        Assert.NotNull(result);
        Assert.False(result.Succeeded);
        _context.Tarefas.Count().Should().Be(TarefaMockData.ObterTodos().Count());

        return Task.CompletedTask;
    }

    [Fact]
    public Task UpdateTarefa_SuccessOnSave()
    {
        /// Arrange
        _context.Tarefas.AddRange(TarefaMockData.ObterTodos());
        _context.SaveChanges();

        var tarefaRepository = new Repository<Tarefa>(_context);
        var pessoaRepository = new Repository<Pessoa>(_context);

        var sut = new TarefaService(tarefaRepository, pessoaRepository);

        var tarefa = TarefaMockData.ObterTodos().ElementAt(0);
        var id = tarefa.Id;
        tarefa.Id = 0;
        tarefa.Descricao = "Nova descrição";
        tarefa.Status = Common.Enums.EnumStatusTarefa.Finalizado;

        /// Act
        var result = sut.UpdateTarefa(id, tarefa);

        ///Assert
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal("Nova descrição", _context.Tarefas.ToList().ElementAt(0).Descricao);
        Assert.Equal(Common.Enums.EnumStatusTarefa.Finalizado, _context.Tarefas.ToList().ElementAt(0).Status);
        _context.Tarefas.Count().Should().Be(TarefaMockData.ObterTodos().Count());

        return Task.CompletedTask;
    }

    [Fact]
    public Task UpdateTarefa_ValidationFail()
    {
        /// Arrange
        _context.Tarefas.AddRange(TarefaMockData.ObterTodos());
        _context.SaveChanges();

        var tarefaRepository = new Repository<Tarefa>(_context);
        var pessoaRepository = new Repository<Pessoa>(_context);

        var sut = new TarefaService(tarefaRepository, pessoaRepository);

        var tarefa = TarefaMockData.ObterTodos().ElementAt(0);
        tarefa.Descricao = "AB";

        /// Act
        var result = sut.UpdateTarefa(tarefa.Id, tarefa);

        ///Assert
        Assert.NotNull(result);
        Assert.False(result.Succeeded);
        _context.Tarefas.Count().Should().Be(TarefaMockData.ObterTodos().Count());

        return Task.CompletedTask;
    }

    [Fact]
    public Task UpdateTarefa_ObjectNotExists()
    {
        /// Arrange
        _context.Tarefas.AddRange(TarefaMockData.ObterTodos());
        _context.SaveChanges();

        var tarefaRepository = new Repository<Tarefa>(_context);
        var pessoaRepository = new Repository<Pessoa>(_context);

        var sut = new TarefaService(tarefaRepository, pessoaRepository);

        var tarefa = TarefaMockData.NewTarefa();

        /// Act
        var result = sut.UpdateTarefa(tarefa.Id, tarefa);

        ///Assert
        Assert.NotNull(result);
        Assert.False(result.Succeeded);
        _context.Tarefas.Count().Should().Be(TarefaMockData.ObterTodos().Count());

        return Task.CompletedTask;
    }

    [Fact]
    public Task DeleteTarefa_SuccessOndelete()
    {
        /// Arrange
        _context.Tarefas.AddRange(TarefaMockData.ObterTodos());
        _context.SaveChanges();

        var tarefaRepository = new Repository<Tarefa>(_context);
        var pessoaRepository = new Repository<Pessoa>(_context);

        var sut = new TarefaService(tarefaRepository, pessoaRepository);

        var tarefa = TarefaMockData.ObterTodos().ElementAt(0);

        /// Act
        var result = sut.DeleteTarefa(tarefa.Id);

        ///Assert
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        _context.Tarefas.Count().Should().Be(TarefaMockData.ObterTodos().Count() - 1);

        return Task.CompletedTask;
    }

    [Fact]
    public Task DeleteTarefa_ObjectNotExists()
    {
        /// Arrange
        _context.Tarefas.AddRange(TarefaMockData.ObterTodos());
        _context.SaveChanges();

        var tarefaRepository = new Repository<Tarefa>(_context);
        var pessoaRepository = new Repository<Pessoa>(_context);

        var sut = new TarefaService(tarefaRepository, pessoaRepository);

        var tarefa = TarefaMockData.NewTarefa();

        /// Act
        var result = sut.DeleteTarefa(tarefa.Id);

        ///Assert
        Assert.NotNull(result);
        Assert.False(result.Succeeded);
        _context.Tarefas.Count().Should().Be(TarefaMockData.ObterTodos().Count());

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}