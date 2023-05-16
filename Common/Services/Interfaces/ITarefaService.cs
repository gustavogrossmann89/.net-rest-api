using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Common.Enums;

using DotNetCore.Results;

namespace TrilhaApiDesafio.Common.Services.Interfaces
{
    public interface ITarefaService
    {
        IResult<Tarefa> CreateTarefa(Tarefa tarefa);
        IResult<Tarefa> UpdateTarefa(int id, Tarefa tarefa);
        DotNetCore.Results.IResult DeleteTarefa(int id);
        Tarefa GetTarefaById(int id);
        IEnumerable<Tarefa> GetAllTarefas();
        IEnumerable<Tarefa> GetTarefaByData(DateTime data);
        IEnumerable<Tarefa> GetTarefaByStatus(EnumStatusTarefa status);
        IEnumerable<Tarefa> GetTarefaByTitulo(string titulo);
    }
}