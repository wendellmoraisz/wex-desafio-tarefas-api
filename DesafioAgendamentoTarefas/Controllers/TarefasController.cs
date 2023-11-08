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
    }
}
