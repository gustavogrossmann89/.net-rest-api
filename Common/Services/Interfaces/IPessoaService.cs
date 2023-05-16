using TrilhaApiDesafio.Models;

using DotNetCore.Results;

namespace TrilhaApiDesafio.Common.Services.Interfaces
{
    public interface IPessoaService
    {
        IResult<Pessoa> Create(Pessoa pessoa);
        DotNetCore.Results.IResult Delete(int id);
        Pessoa GetById(int id);
        IEnumerable<Pessoa> GetAll();
        IEnumerable<Pessoa> GetPessoaByCPF(string CPF);
    }
}