using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Common.Services.Interfaces
{
    public interface IPessoaService
    {
        Pessoa Create(Pessoa pessoa);
        bool Delete(int id);
        IEnumerable<Pessoa> GetAll();
        Pessoa GetById(int id);
    }
}