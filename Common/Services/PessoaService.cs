using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Common.Repositories.Interfaces;
using TrilhaApiDesafio.Common.Services.Interfaces;

namespace TrilhaApiDesafio.Common.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IRepository<Pessoa> _pessoaRepository;
        public PessoaService(IRepository<Pessoa> pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public Pessoa Create(Pessoa tarefa)
        {
            if (tarefa.Id != 0)
                return null;

            Pessoa pessoaSalva = _pessoaRepository.Create(tarefa);
            _pessoaRepository.Save();
            return pessoaSalva;
        }

        public bool Delete(int id)
        {
            var pessoa = _pessoaRepository.GetById(id);
            if (pessoa == null)
                return false;

            _pessoaRepository.Delete(pessoa);
            _pessoaRepository.Save();
            return true;
        }

        public IEnumerable<Pessoa> GetAll()
        {
            return _pessoaRepository.GetAll();
        }

        public Pessoa GetById(int id)
        {
            return _pessoaRepository.GetById(id);
        }
    }
}