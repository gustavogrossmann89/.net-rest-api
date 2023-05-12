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

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            tarefa = _tarefaService.CreateTarefa(tarefa);
            if (tarefa == null)
                return BadRequest(new { Erro = "Dados inválidos para criação" });

            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            tarefa = _tarefaService.UpdateTarefa(id, tarefa);
            if (tarefa == null)
                return BadRequest(new { Erro = "Dados inválidos para atualização" });

            return Ok(tarefa);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var deleted = _tarefaService.DeleteTarefa(id);
            if (!deleted)
                return BadRequest();

            return NoContent();
        }
    }
}
