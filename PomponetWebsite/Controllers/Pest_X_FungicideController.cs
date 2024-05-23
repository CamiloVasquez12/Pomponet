using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PomponetWebsite.Context;
using PomponetWebsite.Models;

namespace PomponetWebsite.Controllers
{
    public class Pest_X_FungicideController : Controller
    {
        private readonly CropsDbContext _context;

        public Pest_X_FungicideController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: Pest_X_Fungicide
        public async Task<IActionResult> Index(int? buscar, string filtro)
        {
            var pest_x_fungicide = from pest_x_fungicid in _context.Pest_X_Fungicide select pest_x_fungicid;

            if (buscar.HasValue && buscar.Value != 0)  // Validación para evitar buscar con valor 0 o nulo
            {
                string buscarStr = buscar.Value.ToString();
                pest_x_fungicide = pest_x_fungicide.Where(s => s.Id_Pest.ToString().Contains(buscarStr));
            }

            ViewData["FiltroId_Pest"] = String.IsNullOrEmpty(filtro) ? "Id_PestDescendente" : "";
            ViewData["FiltroId_Fungicide"] = filtro == "Id_FungicideAscendente" ? "Id_FungicideDescendente" : "Id_FungicideAscendente";

            switch (filtro)
            {
                case "Id_PestDescendente":
                    pest_x_fungicide = pest_x_fungicide.OrderByDescending(pest_x_fungicid => pest_x_fungicid.Id_Pest);
                    break;
                case "Id_FungicideDescendente":
                    pest_x_fungicide = pest_x_fungicide.OrderByDescending(pest_x_fungicid => pest_x_fungicid.Id_Fungicide);
                    break;
                case "Id_FungicideAscendente":
                    pest_x_fungicide = pest_x_fungicide.OrderBy(pest_x_fungicid => pest_x_fungicid.Id_Fungicide);
                    break;
                default:
                    pest_x_fungicide = pest_x_fungicide.OrderBy(pest_x_fungicid => pest_x_fungicid.Id_Pest);
                    break;
            }

            return View(await pest_x_fungicide.ToListAsync());
        }

        // GET: Pest_X_Fungicide/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pest_X_Fungicide = await _context.Pest_X_Fungicide
                .FirstOrDefaultAsync(m => m.Id_Pest_X_Fungicide == id);
            if (pest_X_Fungicide == null)
            {
                return NotFound();
            }

            return View(pest_X_Fungicide);
        }

        // GET: Pest_X_Fungicide/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pest_X_Fungicide/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Pest_X_Fungicide,Id_Pest,Id_Fungicide,Deleted")] Pest_X_Fungicide pest_X_Fungicide)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pest_X_Fungicide);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pest_X_Fungicide);
        }

        // GET: Pest_X_Fungicide/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pest_X_Fungicide = await _context.Pest_X_Fungicide.FindAsync(id);
            if (pest_X_Fungicide == null)
            {
                return NotFound();
            }
            return View(pest_X_Fungicide);
        }

        // POST: Pest_X_Fungicide/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Pest_X_Fungicide,Id_Pest,Id_Fungicide,Deleted")] Pest_X_Fungicide pest_X_Fungicide)
        {
            if (id != pest_X_Fungicide.Id_Pest_X_Fungicide)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pest_X_Fungicide);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Pest_X_FungicideExists(pest_X_Fungicide.Id_Pest_X_Fungicide))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pest_X_Fungicide);
        }

        // GET: Pest_X_Fungicide/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pest_X_Fungicide = await _context.Pest_X_Fungicide
                .FirstOrDefaultAsync(m => m.Id_Pest_X_Fungicide == id);
            if (pest_X_Fungicide == null)
            {
                return NotFound();
            }

            return View(pest_X_Fungicide);
        }

        // POST: Pest_X_Fungicide/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pest_x_fungicid = await _context.Pest_X_Fungicide.FindAsync(id);
            if (pest_x_fungicid != null)
            {
                pest_x_fungicid.Deleted = true; // Marca el registro como eliminado
                _context.Pest_X_Fungicide.Update(pest_x_fungicid); // Actualiza el registro en el contexto
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Pest_X_FungicideExists(int id)
        {
            return _context.Pest_X_Fungicide.Any(e => e.Id_Pest_X_Fungicide == id);
        }
    }
}
