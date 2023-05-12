using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Common.Enums;

namespace TrilhaApiDesafio.Common.Services.Interfaces
{
    public interface ITarefaService
    {
        Tarefa CreateTarefa(Tarefa tarefa);
        bool DeleteTarefa(int id);
        IEnumerable<Tarefa> GetAllTarefas();
        IEnumerable<Tarefa> GetTarefaByData(DateTime data);
        Tarefa GetTarefaById(int id);
        IEnumerable<Tarefa> GetTarefaByStatus(EnumStatusTarefa status);
        IEnumerable<Tarefa> GetTarefaByTitulo(string titulo);
        Tarefa UpdateTarefa(int id, Tarefa tarefa);
    }
}