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
    public class PestsController : Controller
    {
        private readonly CropsDbContext _context;

        public PestsController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: Pests
        public async Task<IActionResult> Index(string buscar, string filtro)
        {
            var pests = from pest in _context.Pests select pest;
            if (!String.IsNullOrEmpty(buscar))
            {
                pests = pests.Where(s => s.Pest!.Contains(buscar));
            }
            ViewData["FiltroPest"] = String.IsNullOrEmpty(filtro) ? "PestDescendente" : "";

            switch (filtro)
            {
                case "PestDescendente":
                    pests = pests.OrderByDescending(pests => pests.Pest);
                    break;
                default:
                    pests = pests.OrderBy(pests => pests.Pest);
                    break;
            }
            return View(await pests.ToListAsync());
        }

        // GET: Pests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pests = await _context.Pests
                .FirstOrDefaultAsync(m => m.Id_Pest == id);
            if (pests == null)
            {
                return NotFound();
            }

            return View(pests);
        }

        // GET: Pests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Pest,Pest,Deleted")] Pests pests)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pests);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pests);
        }

        // GET: Pests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pests = await _context.Pests.FindAsync(id);
            if (pests == null)
            {
                return NotFound();
            }
            return View(pests);
        }

        // POST: Pests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Pest,Pest,Deleted")] Pests pests)
        {
            if (id != pests.Id_Pest)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pests);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PestsExists(pests.Id_Pest))
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
            return View(pests);
        }

        // GET: Pests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pests = await _context.Pests
                .FirstOrDefaultAsync(m => m.Id_Pest == id);
            if (pests == null)
            {
                return NotFound();
            }

            return View(pests);
        }

        // POST: Pests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pests = await _context.Pests.FindAsync(id);
            if (pests != null)
            {
                pests.Deleted = true; // Marca el registro como eliminado
                _context.Pests.Update(pests); // Actualiza el registro en el contexto
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PestsExists(int id)
        {
            return _context.Pests.Any(e => e.Id_Pest == id);
        }
    }
}
