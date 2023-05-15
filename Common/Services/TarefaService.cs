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

            Tarefa tarefaSalva = _tarefaRepository.Create(tarefa);
            _tarefaRepository.Save();

            if (tarefaSalva.ResponsavelId != null)
            {
                var pessoa = _pessoaRepository.GetById(tarefaSalva.ResponsavelId);
                tarefaSalva.Responsavel = pessoa;
            }

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
            var tarefas = _tarefaRepository.GetAll();
            tarefas.ToList().ForEach(item =>
            {
                if (item.ResponsavelId != null)
                {
                    var pessoa = _pessoaRepository.GetById(item.ResponsavelId);
                    item.Responsavel = pessoa;
                }
            });
            return tarefas;
        }

        public IEnumerable<Tarefa> GetTarefaByData(DateTime data)
        {
            var tarefas = _tarefaRepository.Get(x => x.Data.Date == data.Date);
            tarefas.ToList().ForEach(item =>
            {
                if (item.ResponsavelId != null)
                {
                    var pessoa = _pessoaRepository.GetById(item.ResponsavelId);
                    item.Responsavel = pessoa;
                }
            });
            return tarefas;
        }

        public Tarefa GetTarefaById(int id)
        {
            var tarefa = _tarefaRepository.GetById(id);
            if (tarefa.ResponsavelId != null)
            {
                var pessoa = _pessoaRepository.GetById(tarefa.ResponsavelId);
                tarefa.Responsavel = pessoa;
            }

            return tarefa;
        }

        public IEnumerable<Tarefa> GetTarefaByStatus(EnumStatusTarefa status)
        {
            var tarefas = _tarefaRepository.Get(x => x.Status == status);
            tarefas.ToList().ForEach(item =>
            {
                if (item.ResponsavelId != null)
                {
                    var pessoa = _pessoaRepository.GetById(item.ResponsavelId);
                    item.Responsavel = pessoa;
                }
            });
            return tarefas;
        }

        public IEnumerable<Tarefa> GetTarefaByTitulo(string titulo)
        {
            var tarefas = _tarefaRepository.Get(x => x.Titulo.ToUpper().Contains(titulo.ToUpper()));
            tarefas.ToList().ForEach(item =>
            {
                if (item.ResponsavelId != null)
                {
                    var pessoa = _pessoaRepository.GetById(item.ResponsavelId);
                    item.Responsavel = pessoa;
                }
            });
            return tarefas;
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

            if (tarefaBanco.ResponsavelId != null)
            {
                var pessoa = _pessoaRepository.GetById(tarefaBanco.ResponsavelId);
                tarefaBanco.Responsavel = pessoa;
            }

            return tarefaBanco;
        }
    }
}