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

        [HttpPost]
        public IActionResult Criar(Pessoa pessoa)
        {
            var result = _pessoaService.Create(pessoa);
            if (result.Succeeded)
                return Created($"/{result.Data.Id}", pessoa);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var result = _pessoaService.Delete(id);
            if (result.Succeeded)
                return NoContent();
            return BadRequest(result);
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
    }
}
