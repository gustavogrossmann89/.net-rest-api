using TrilhaApiDesafio.Common.Services;
using TrilhaApiDesafio.Common.Repositories;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.MockData;
using TrilhaApiDesafio.Context;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TrilhaApiDesafio.Tests;

public class TestPessoaService : IDisposable
{
    protected readonly OrganizadorContext _context;
    public TestPessoaService()
    {
        var options = new DbContextOptionsBuilder<OrganizadorContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        _context = new OrganizadorContext(options);

        _context.Database.EnsureCreated();
    }

    [Fact]
    public Task GetAll_ReturnPessoaCollection()
    {
        /// Arrange
        _context.Pessoas.AddRange(PessoaMockData.GetAll());
        _context.SaveChanges();

        var pessoaRepository = new Repository<Pessoa>(_context);
        var sut = new PessoaService(pessoaRepository);

        /// Act
        var result = sut.GetAll();

        /// Assert
        result.Should().HaveCount(PessoaMockData.GetAll().Count);
        return Task.CompletedTask;
    }

    [Fact]
    public Task GetPessoaById_ReturnPessoa()
    {
        /// Arrange
        _context.Pessoas.AddRange(PessoaMockData.GetAll());
        _context.SaveChanges();

        var pessoaRepository = new Repository<Pessoa>(_context);
        var sut = new PessoaService(pessoaRepository);
        var pessoa = PessoaMockData.GetAll().ElementAt(0);

        /// Act
        var result = sut.GetById(pessoa.Id);

        /// Assert
        Assert.NotNull(result);
        Assert.Equal(PessoaMockData.GetAll().ElementAt(0).Id, result.Id);
        Assert.Equal(PessoaMockData.GetAll().ElementAt(0).CPF, result.CPF);
        return Task.CompletedTask;
    }

    [Fact]
    public Task GetTarefaById_ObjectNotExists()
    {
        /// Arrange
        _context.Pessoas.AddRange(PessoaMockData.GetAll());
        _context.SaveChanges();

        var pessoaRepository = new Repository<Pessoa>(_context);
        var sut = new PessoaService(pessoaRepository);
        var pessoa = PessoaMockData.NewPessoa();

        /// Act
        var result = sut.GetById(pessoa.Id);

        /// Assert
        Assert.Null(result);
        return Task.CompletedTask;
    }

    [Fact]
    public Task Create_SuccessOnSave()
    {
        /// Arrange
        _context.Pessoas.AddRange(PessoaMockData.GetAll());
        _context.SaveChanges();

        var pessoaRepository = new Repository<Pessoa>(_context);
        var sut = new PessoaService(pessoaRepository);
        var newPessoa = PessoaMockData.NewPessoa();

        /// Act
        var result = sut.Create(newPessoa);

        ///Assert
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        int expectedRecordCount = (PessoaMockData.GetAll().Count() + 1);
        _context.Pessoas.Count().Should().Be(expectedRecordCount);

        return Task.CompletedTask;
    }

    [Fact]
    public Task Create_ValidationFail()
    {
        /// Arrange
        _context.Pessoas.AddRange(PessoaMockData.GetAll());
        _context.SaveChanges();

        var pessoaRepository = new Repository<Pessoa>(_context);
        var sut = new PessoaService(pessoaRepository);
        var newPessoa = PessoaMockData.NewIncompletePessoa();

        /// Act
        var result = sut.Create(newPessoa);

        ///Assert
        Assert.NotNull(result);
        Assert.False(result.Succeeded);
        _context.Pessoas.Count().Should().Be(PessoaMockData.GetAll().Count());

        return Task.CompletedTask;
    }

    [Fact]
    public Task Delete_SuccessOndelete()
    {
        /// Arrange
        _context.Pessoas.AddRange(PessoaMockData.GetAll());
        _context.SaveChanges();

        var pessoaRepository = new Repository<Pessoa>(_context);
        var sut = new PessoaService(pessoaRepository);
        var pessoa = PessoaMockData.GetAll().ElementAt(0);

        /// Act
        var result = sut.Delete(pessoa.Id);

        ///Assert
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        _context.Pessoas.Count().Should().Be(PessoaMockData.GetAll().Count() - 1);

        return Task.CompletedTask;
    }

    [Fact]
    public Task Delete_ObjectNotExists()
    {
        //// Arrange
        _context.Pessoas.AddRange(PessoaMockData.GetAll());
        _context.SaveChanges();

        var pessoaRepository = new Repository<Pessoa>(_context);
        var sut = new PessoaService(pessoaRepository);
        var pessoa = PessoaMockData.NewPessoa();

        /// Act
        var result = sut.Delete(pessoa.Id);

        ///Assert
        Assert.NotNull(result);
        Assert.False(result.Succeeded);
        _context.Pessoas.Count().Should().Be(PessoaMockData.GetAll().Count());

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}