using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Common.Repositories.Interfaces;
using TrilhaApiDesafio.Common.Services.Interfaces;

using DotNetCore.Validation;
using DotNetCore.Results;

namespace TrilhaApiDesafio.Common.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IRepository<Pessoa> _pessoaRepository;
        public PessoaService(IRepository<Pessoa> pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public IResult<Pessoa> Create(Pessoa pessoa)
        {
            var validation = new PessoaValidator().Validation(pessoa);
            if (validation.Failed)
                return validation.Fail<Pessoa>();

            var pessoasExistentes = _pessoaRepository.Get(x => x.CPF == pessoa.CPF);
            if (pessoasExistentes.ToList().Count() > 0)
                return Result<Pessoa>.Fail("já existe pessoa cadastrada com este CPF");

            pessoa = _pessoaRepository.Create(pessoa);
            _pessoaRepository.Save();
            return pessoa.Success("Pessoa criada com sucesso");
        }

        public DotNetCore.Results.IResult Delete(int id)
        {
            var pessoa = _pessoaRepository.GetById(id);
            if (pessoa == null)
                return Result.Fail("Pessoa não encontrada");

            _pessoaRepository.Delete(pessoa);
            _pessoaRepository.Save();
            return Result.Success();
        }

        public Pessoa GetById(int id)
        {
            return _pessoaRepository.GetById(id);
        }

        public IEnumerable<Pessoa> GetAll()
        {
            return _pessoaRepository.GetAll();
        }

        public IEnumerable<Pessoa> GetPessoaByCPF(string CPF)
        {
            return _pessoaRepository.Get(x => x.CPF == CPF);
        }
    }
}