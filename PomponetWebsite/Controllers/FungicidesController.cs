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
    public class FungicidesController : Controller
    {
        private readonly CropsDbContext _context;

        public FungicidesController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: Fungicides
        public async Task<IActionResult> Index(string buscar, string filtro)
        {
            var fungicides = from fungicide in _context.Fungicides select fungicide;
            if (!String.IsNullOrEmpty(buscar))
            {
                fungicides = fungicides.Where(s => s.Name_Fungicide!.Contains(buscar));
            }
            ViewData["FiltroName_Fungicide"] = String.IsNullOrEmpty(filtro) ? "Name_FungicideDescendente" : "";
            ViewData["FiltroId_Crop"] = filtro == "Id_CropAscendente" ? "Id_CropDescendente" : "Id_CropAscendente";

            switch (filtro)
            {
                case "Name_FungicideDescendente":
                    fungicides = fungicides.OrderByDescending(fungicides => fungicides.Name_Fungicide);
                    break;
                case "Id_CropDescendente":
                    fungicides = fungicides.OrderByDescending(fungicides => fungicides.Id_crop);
                    break;
                case "Id_CropAscendente":
                    fungicides = fungicides.OrderBy(fungicides => fungicides.Id_crop);
                    break;
                default:
                    fungicides = fungicides.OrderBy(fungicides => fungicides.Name_Fungicide);
                    break;
            }
            return View(await fungicides.ToListAsync());
        }

        // GET: Fungicides/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fungicides = await _context.Fungicides
                .FirstOrDefaultAsync(m => m.Id_Fungicide == id);
            if (fungicides == null)
            {
                return NotFound();
            }

            return View(fungicides);
        }

        // GET: Fungicides/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fungicides/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Fungicide,Name_Fungicide,Quantity,Description,Id_crop,Deleted")] Fungicides fungicides)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fungicides);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fungicides);
        }

        // GET: Fungicides/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fungicides = await _context.Fungicides.FindAsync(id);
            if (fungicides == null)
            {
                return NotFound();
            }
            return View(fungicides);
        }

        // POST: Fungicides/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Fungicide,Name_Fungicide,Quantity,Description,Id_crop,Deleted")] Fungicides fungicides)
        {
            if (id != fungicides.Id_Fungicide)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fungicides);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FungicidesExists(fungicides.Id_Fungicide))
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
            return View(fungicides);
        }

        // GET: Fungicides/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fungicides = await _context.Fungicides
                .FirstOrDefaultAsync(m => m.Id_Fungicide == id);
            if (fungicides == null)
            {
                return NotFound();
            }

            return View(fungicides);
        }

        // POST: Fungicides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fungicides = await _context.Fungicides.FindAsync(id);
            if (fungicides != null)
            {
                fungicides.Deleted = true; // Marca el registro como eliminado
                _context.Fungicides.Update(fungicides); // Actualiza el registro en el contexto
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FungicidesExists(int id)
        {
            return _context.Fungicides.Any(e => e.Id_Fungicide == id);
        }
    }
}
