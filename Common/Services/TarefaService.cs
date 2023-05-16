using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Common.Repositories.Interfaces;
using TrilhaApiDesafio.Common.Services.Interfaces;
using TrilhaApiDesafio.Common.Enums;

using DotNetCore.Validation;
using DotNetCore.Results;

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

        public IResult<Tarefa> CreateTarefa(Tarefa tarefa)
        {
            var validation = new TarefaValidator().Validation(tarefa);
            if (validation.Failed)
                return validation.Fail<Tarefa>();

            if (tarefa.ResponsavelId != null)
            {
                var pessoa = _pessoaRepository.GetById(tarefa.ResponsavelId);
                if (pessoa != null)
                    tarefa.Responsavel = pessoa;
                else
                    tarefa.ResponsavelId = null;
            }

            Tarefa tarefaSalva = _tarefaRepository.Create(tarefa);
            _tarefaRepository.Save();

            return tarefaSalva.Success("Tarefa criada com sucesso");
        }

        public IResult<Tarefa> UpdateTarefa(int id, Tarefa tarefa)
        {
            var validation = new TarefaValidator().Validation(tarefa);
            if (validation.Failed)
                return validation.Fail<Tarefa>();

            var tarefaBanco = _tarefaRepository.GetById(id);
            if (tarefaBanco == null)
                return Result<Tarefa>.Fail("Tarefa não encontrada");

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            if (tarefa.ResponsavelId != null)
            {
                var pessoa = _pessoaRepository.GetById(tarefa.ResponsavelId);
                if (pessoa != null)
                {
                    tarefaBanco.Responsavel = pessoa;
                    tarefaBanco.ResponsavelId = tarefa.ResponsavelId;
                }
                else
                {
                    tarefaBanco.Responsavel = null;
                    tarefaBanco.ResponsavelId = null;
                }
            }

            _tarefaRepository.Update(tarefaBanco);
            _tarefaRepository.Save();

            if (tarefaBanco.ResponsavelId != null)
            {
                var pessoa = _pessoaRepository.GetById(tarefaBanco.ResponsavelId);
                tarefaBanco.Responsavel = pessoa;
            }

            return tarefaBanco.Success("Tarefa atualizada com sucesso");
        }

        public DotNetCore.Results.IResult DeleteTarefa(int id)
        {
            var tarefa = _tarefaRepository.GetById(id);
            if (tarefa == null)
                return Result.Fail("Tarefa não encontrada");

            _tarefaRepository.Delete(tarefa);
            _tarefaRepository.Save();
            return Result.Success();
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
    }
}