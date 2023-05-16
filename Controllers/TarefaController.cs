using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Common.Enums;
using TrilhaApiDesafio.Common.Services.Interfaces;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            var result = _tarefaService.CreateTarefa(tarefa);
            if (result.Succeeded)
                return Created($"/{result.Data.Id}", tarefa);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var result = _tarefaService.UpdateTarefa(id, tarefa);
            if (result.Succeeded)
                return Ok(tarefa);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var result = _tarefaService.DeleteTarefa(id);
            if (result.Succeeded)
                return NoContent();
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var result = _tarefaService.GetTarefaById(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var result = _tarefaService.GetAllTarefas();
            return new JsonResult(Ok(result));
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var result = _tarefaService.GetTarefaByTitulo(titulo);
            if (result.Count() == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var result = _tarefaService.GetTarefaByData(data);
            if (result.Count() == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            var result = _tarefaService.GetTarefaByStatus(status);
            if (result.Count() == 0)
                return NotFound();

            return Ok(result);
        }
    }
}
