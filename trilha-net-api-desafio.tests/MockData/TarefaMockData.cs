using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Common.Enums;

namespace TrilhaApiDesafio.MockData;
public class TarefaMockData
{
    public static List<Tarefa> ObterTodos()
    {
        return new List<Tarefa>{
                new Tarefa{
                    Id = 1,
                    Data = DateTime.Parse("2023-05-11T20:00:00.000Z"),
                    Titulo = "Tarefa Teste 1",
                    Descricao = "Descricao da Tarefa Teste 1",
                    Status = EnumStatusTarefa.Pendente
                },
                new Tarefa{
                    Id = 2,
                    Data = DateTime.Parse("2023-05-12T20:00:00.000Z"),
                    Titulo = "Tarefa Teste 2",
                    Descricao = "Descricao da Tarefa Teste 2",
                    Status = EnumStatusTarefa.Pendente
                },
                new Tarefa{
                    Id = 3,
                    Data = DateTime.Parse("2023-05-13T20:00:00.000Z"),
                    Titulo = "Tarefa Teste 3",
                    Descricao = "Descricao da Tarefa Teste 3",
                    Status = EnumStatusTarefa.Finalizado
                },
            };
    }

    public static Tarefa NewTarefa()
    {
        return new Tarefa
        {
            Id = 0,
            Data = DateTime.Now,
            Titulo = "Nova Tarefa Teste",
            Descricao = "Descricao da Nova Tarefa Teste",
            Status = EnumStatusTarefa.Pendente
        };
    }

    public static Tarefa NewIncompleteTarefa()
    {
        return new Tarefa
        {
            Id = 1,
            Data = DateTime.Now,
        };
    }
}