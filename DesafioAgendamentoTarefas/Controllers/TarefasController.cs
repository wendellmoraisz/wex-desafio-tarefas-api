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
    public class TarefasController : Controller
    {
        private readonly OrganizadorContext _context;

        public TarefasController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
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
    }
}
