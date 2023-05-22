using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Common.Enums;

namespace TrilhaApiDesafio.MockData;
public class PessoaMockData
{
    public static List<Pessoa> GetAll()
    {
        return new List<Pessoa>{
                new Pessoa{
                    Id = 1,
                    Nome = "Pessoa 1",
                    CPF = "98765432109",
                    DataNascimento = DateTime.Parse("2000-01-01T20:00:00.000Z"),
                },
                new Pessoa{
                    Id = 2,
                    Nome = "Pessoa 2",
                    CPF = "98765432107",
                    DataNascimento = DateTime.Parse("2000-01-01T20:00:00.000Z"),
                },
                new Pessoa{
                    Id = 3,
                    Nome = "Pessoa 3",
                    CPF = "98765432108",
                    DataNascimento = DateTime.Parse("2000-01-01T20:00:00.000Z"),
                },
            };
    }

    public static Pessoa NewPessoa()
    {
        return new Pessoa
        {
            Id = 0,
            Nome = "Pessoa 99",
            CPF = "98765432199",
            DataNascimento = DateTime.Parse("2000-01-01T20:00:00.000Z"),
        };
    }

    public static Pessoa NewIncompletePessoa()
    {
        return new Pessoa
        {
            Id = 1,
            DataNascimento = DateTime.Parse("2000-01-01T20:00:00.000Z"),
        };
    }
}