using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Common.Repositories.Interfaces;
using TrilhaApiDesafio.Common.Services.Interfaces;
using TrilhaApiDesafio.Common.Enums;

namespace TrilhaApiDesafio.Common.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly IRepository<Tarefa> _tarefaRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        public TarefaService(IRepository<Tarefa> tarefaRepository, IRepository<Pessoa> pessoaRepository)
        {
            _tarefaRepository = tarefaRepository;
            _pessoaRepository = pessoaRepository;
        }

        public Tarefa CreateTarefa(Tarefa tarefa)
        {
            if (tarefa.Id != 0 || tarefa.Data == DateTime.MinValue)
                return null;

            if (tarefa.Responsavel != null)
            {
                var pessoa = _pessoaRepository.GetById(tarefa.Responsavel);
                //Console.WriteLine(pessoa);
            }

            Tarefa tarefaSalva = _tarefaRepository.Create(tarefa);
            _tarefaRepository.Save();
            return tarefaSalva;
        }

        public bool DeleteTarefa(int id)
        {
            var tarefa = _tarefaRepository.GetById(id);
            if (tarefa == null)
                return false;

            _tarefaRepository.Delete(tarefa);
            _tarefaRepository.Save();
            return true;
        }

        public IEnumerable<Tarefa> GetAllTarefas()
        {
            return _tarefaRepository.GetAll();
        }

        public IEnumerable<Tarefa> GetTarefaByData(DateTime data)
        {
            return _tarefaRepository.Get(x => x.Data.Date == data.Date);
        }

        public Tarefa GetTarefaById(int id)
        {
            return _tarefaRepository.GetById(id);
        }

        public IEnumerable<Tarefa> GetTarefaByStatus(EnumStatusTarefa status)
        {
            return _tarefaRepository.Get(x => x.Status == status);
        }

        public IEnumerable<Tarefa> GetTarefaByTitulo(string titulo)
        {
            return _tarefaRepository.Get(x => x.Titulo.ToUpper().Contains(titulo.ToUpper()));
        }

        public Tarefa UpdateTarefa(int id, Tarefa tarefa)
        {
            var tarefaBanco = _tarefaRepository.GetById(id);
            if (tarefaBanco == null)
                return null;

            if (tarefa.Data == DateTime.MinValue)
                return null;

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _tarefaRepository.Update(tarefaBanco);
            _tarefaRepository.Save();
            return tarefaBanco;
        }
    }
}