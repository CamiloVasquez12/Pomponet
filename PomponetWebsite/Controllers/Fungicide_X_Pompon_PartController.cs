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
    public class Fungicide_X_Pompon_PartController : Controller
    {
        private readonly CropsDbContext _context;

        public Fungicide_X_Pompon_PartController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: Fungicide_X_Pompon_Part
        public async Task<IActionResult> Index(int? buscar, string filtro)
        {
            var fungicide_x_pompon_part = from fungicide_x_pompon_par in _context.Fungicide_X_Pompon_Part select fungicide_x_pompon_par;

            if (buscar.HasValue && buscar.Value != 0)  // Validación para evitar buscar con valor 0 o nulo
            {
                string buscarStr = buscar.Value.ToString();
                fungicide_x_pompon_part = fungicide_x_pompon_part.Where(s => s.Id_Pompon_Part.ToString().Contains(buscarStr));
            }

            ViewData["FiltroId_Pompon_Part"] = String.IsNullOrEmpty(filtro) ? "Id_Pompon_PartDescendente" : "";
            ViewData["FiltroId_Fungicide"] = filtro == "Id_FungicideAscendente" ? "Id_FungicideDescendente" : "Id_FungicideAscendente";

            switch (filtro)
            {
                case "Id_Pompon_PartDescendente":
                    fungicide_x_pompon_part = fungicide_x_pompon_part.OrderByDescending(fungicide_x_pompon_par => fungicide_x_pompon_par.Id_Pompon_Part);
                    break;
                case "Id_FungicideDescendente":
                    fungicide_x_pompon_part = fungicide_x_pompon_part.OrderByDescending(fungicide_x_pompon_par => fungicide_x_pompon_par.Id_Fungicide);
                    break;
                case "Id_FungicideAscendente":
                    fungicide_x_pompon_part = fungicide_x_pompon_part.OrderBy(fungicide_x_pompon_par => fungicide_x_pompon_par.Id_Fungicide);
                    break;
                default:
                    fungicide_x_pompon_part = fungicide_x_pompon_part.OrderBy(fungicide_x_pompon_par => fungicide_x_pompon_par.Id_Pompon_Part);
                    break;
            }

            return View(await fungicide_x_pompon_part.ToListAsync());
        }

        // GET: Fungicide_X_Pompon_Part/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fungicide_X_Pompon_Part = await _context.Fungicide_X_Pompon_Part
                .FirstOrDefaultAsync(m => m.Id_Fungicide_X_Pompon_Part == id);
            if (fungicide_X_Pompon_Part == null)
            {
                return NotFound();
            }

            return View(fungicide_X_Pompon_Part);
        }

        // GET: Fungicide_X_Pompon_Part/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fungicide_X_Pompon_Part/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Fungicide_X_Pompon_Part,Id_Pompon_Part,Id_Fungicide,Deleted")] Fungicide_X_Pompon_Part fungicide_X_Pompon_Part)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fungicide_X_Pompon_Part);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fungicide_X_Pompon_Part);
        }

        // GET: Fungicide_X_Pompon_Part/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fungicide_X_Pompon_Part = await _context.Fungicide_X_Pompon_Part.FindAsync(id);
            if (fungicide_X_Pompon_Part == null)
            {
                return NotFound();
            }
            return View(fungicide_X_Pompon_Part);
        }

        // POST: Fungicide_X_Pompon_Part/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Fungicide_X_Pompon_Part,Id_Pompon_Part,Id_Fungicide,Deleted")] Fungicide_X_Pompon_Part fungicide_X_Pompon_Part)
        {
            if (id != fungicide_X_Pompon_Part.Id_Fungicide_X_Pompon_Part)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fungicide_X_Pompon_Part);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Fungicide_X_Pompon_PartExists(fungicide_X_Pompon_Part.Id_Fungicide_X_Pompon_Part))
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
            return View(fungicide_X_Pompon_Part);
        }

        // GET: Fungicide_X_Pompon_Part/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fungicide_X_Pompon_Part = await _context.Fungicide_X_Pompon_Part
                .FirstOrDefaultAsync(m => m.Id_Fungicide_X_Pompon_Part == id);
            if (fungicide_X_Pompon_Part == null)
            {
                return NotFound();
            }

            return View(fungicide_X_Pompon_Part);
        }

        // POST: Fungicide_X_Pompon_Part/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fungicide_x_pompon_par = await _context.Fungicide_X_Pompon_Part.FindAsync(id);
            if (fungicide_x_pompon_par != null)
            {
                fungicide_x_pompon_par.Deleted = true; // Marca el registro como eliminado
                _context.Fungicide_X_Pompon_Part.Update(fungicide_x_pompon_par); // Actualiza el registro en el contexto
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Fungicide_X_Pompon_PartExists(int id)
        {
            return _context.Fungicide_X_Pompon_Part.Any(e => e.Id_Fungicide_X_Pompon_Part == id);
        }
    }
}
