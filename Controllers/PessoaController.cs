using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Common.Services.Interfaces;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var result = _pessoaService.GetById(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var result = _pessoaService.GetAll();
            return new JsonResult(Ok(result));
        }

        [HttpPost]
        public IActionResult Criar(Pessoa pessoa)
        {
            pessoa = _pessoaService.Create(pessoa);
            if (pessoa == null)
                return BadRequest(new { Erro = "Dados inválidos para criação" });

            return CreatedAtAction(nameof(ObterPorId), new { id = pessoa.Id }, pessoa);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var deleted = _pessoaService.Delete(id);
            if (!deleted)
                return BadRequest();

            return NoContent();
        }
    }
}
