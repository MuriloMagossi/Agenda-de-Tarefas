using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaTarefas.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaTarefas.Controllers
{
    public class TarefasController : Controller
    {
        private readonly Contexto _contexto;

        public TarefasController(Contexto contexto)
        {
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            return View(GetData());
        }

        private List<DatasViewModel> GetData()
        {
            DateTime nowadays = DateTime.Now;
            DateTime limiteDate = DateTime.Now.AddDays(10);
            int qntDays = 0;
            DatasViewModel data;
            List<DatasViewModel> listDates = new List<DatasViewModel>();

            while (nowadays < limiteDate)
            {
                data = new DatasViewModel();
                data.Dates = nowadays.ToShortDateString();
                data.Identifiers = "collapse" + nowadays.ToShortDateString().Replace("/", "");
                listDates.Add(data);
                qntDays = qntDays + 1;
                nowadays = DateTime.Now.AddDays(qntDays);
            }

            return listDates;
        }

        [HttpGet]
        public IActionResult CriarTarefa(string startTime, string endTime)
        {
            Tarefa tarefa = new Tarefa
            {
                StartTime = startTime,
                EndTime = endTime
            };

            return View(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> CriarTarefa(Tarefa tarefa)
        {
            //Tarefa mergeTime = await _contexto.Tarefas.FindAsync(tarefa.StartTime);

            /*if (mergeTime == tarefa.StartTime)
            {
                return NotFound();
            }*/


            if (ModelState.IsValid)
            {
                _contexto.Tarefas.Add(tarefa);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarTarefa(int tarefaId)
        {
            Tarefa tarefa = await _contexto.Tarefas.FindAsync(tarefaId);

            if (tarefa == null)
                return NotFound();

            return View(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarTarefa(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _contexto.Update(tarefa);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);
        }

        [HttpPost]
        public async Task<JsonResult> ExcluirTarefa(int tarefaId)
        {
            Tarefa tarefa = await _contexto.Tarefas.FindAsync(tarefaId);
            _contexto.Tarefas.Remove(tarefa);
            await _contexto.SaveChangesAsync();
            return Json(true);
        }
    }
}