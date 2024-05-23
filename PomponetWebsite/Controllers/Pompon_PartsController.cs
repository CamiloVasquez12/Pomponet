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
    public class Pompon_PartsController : Controller
    {
        private readonly CropsDbContext _context;

        public Pompon_PartsController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: Pompon_Parts
        public async Task<IActionResult> Index(string buscar, string filtro)
        {
            var pompon_parts = from pompon_part in _context.Pompon_Parts select pompon_part;
            if (!String.IsNullOrEmpty(buscar))
            {
                pompon_parts = pompon_parts.Where(s => s.Part!.Contains(buscar));
            }
            ViewData["FiltroPart"] = String.IsNullOrEmpty(filtro) ? "PartDescendente" : "";
            switch (filtro)
            {
                case "PartDescendente":
                    pompon_parts = pompon_parts.OrderByDescending(pompon_parts => pompon_parts.Part);
                    break;
                default:
                    pompon_parts = pompon_parts.OrderBy(pompon_parts => pompon_parts.Part);
                    break;
            }
            return View(await pompon_parts.ToListAsync());
        }

        // GET: Pompon_Parts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pompon_Parts = await _context.Pompon_Parts
                .FirstOrDefaultAsync(m => m.Id_Pompon_Part == id);
            if (pompon_Parts == null)
            {
                return NotFound();
            }

            return View(pompon_Parts);
        }

        // GET: Pompon_Parts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pompon_Parts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Pompon_Part,Part,Deleted")] Pompon_Parts pompon_Parts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pompon_Parts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pompon_Parts);
        }

        // GET: Pompon_Parts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pompon_Parts = await _context.Pompon_Parts.FindAsync(id);
            if (pompon_Parts == null)
            {
                return NotFound();
            }
            return View(pompon_Parts);
        }

        // POST: Pompon_Parts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Pompon_Part,Part,Deleted")] Pompon_Parts pompon_Parts)
        {
            if (id != pompon_Parts.Id_Pompon_Part)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pompon_Parts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Pompon_PartsExists(pompon_Parts.Id_Pompon_Part))
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
            return View(pompon_Parts);
        }

        // GET: Pompon_Parts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pompon_Parts = await _context.Pompon_Parts
                .FirstOrDefaultAsync(m => m.Id_Pompon_Part == id);
            if (pompon_Parts == null)
            {
                return NotFound();
            }

            return View(pompon_Parts);
        }

        // POST: Pompon_Parts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pompon_parts = await _context.Pompon_Parts.FindAsync(id);
            if (pompon_parts != null)
            {
                pompon_parts.Deleted = true; // Marca el registro como eliminado
                _context.Pompon_Parts.Update(pompon_parts); // Actualiza el registro en el contexto
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Pompon_PartsExists(int id)
        {
            return _context.Pompon_Parts.Any(e => e.Id_Pompon_Part == id);
        }
    }
}
