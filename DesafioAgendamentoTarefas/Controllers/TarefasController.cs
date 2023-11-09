using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DesafioAgendamentoTarefas.Context;
using DesafioAgendamentoTarefas.Models;

namespace DesafioAgendamentoTarefas.Controllers
{
    [Route("Tarefas")]
    public class TarefasController : Controller
    {
        private readonly OrganizadorContext _context;

        public TarefasController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ObterPorId([FromQuery] int id)
        {
            var tarefa = _context.Tarefas.Find(id);
           
            if (tarefa == null) return NotFound();
            
            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var tarefas = _context.Tarefas.ToList();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefa = _context.Tarefas.Where(x => x.Titulo == titulo);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EStatusTarefa status)
        {
            var tarefas = _context.Tarefas.Where(x => x.Status == status);
            return Ok(tarefas);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut]
        public IActionResult Atualizar([FromQuery] int id, [FromBody] Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Status = tarefa.Status;
            tarefaBanco.Data = tarefa.Data;

            _context.Update(tarefaBanco);
            _context.SaveChanges();
            
            return Ok(tarefa);
        }

        [HttpDelete]
        public IActionResult Deletar([FromQuery] int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            _context.Remove(tarefaBanco);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
