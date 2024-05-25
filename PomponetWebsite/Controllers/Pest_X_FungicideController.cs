using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["PestSortParm"] = string.IsNullOrEmpty(sortOrder) ? "pest_desc" : "";
            ViewData["FungicideSortParm"] = sortOrder == "fungicide" ? "fungicide_desc" : "fungicide";

            var query = _context.Pest_X_Fungicide
                                .Include(f => f.Pests)
                                .Include(f => f.Fungicides)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(f => f.Pests.Pest.Contains(searchString) ||
                                         f.Fungicides.Name_Fungicide.Contains(searchString));
            }

            query = sortOrder switch
            {
                "pest_desc" => query.OrderByDescending(f => f.Pests.Pest),
                "fungicide" => query.OrderBy(f => f.Fungicides.Name_Fungicide),
                "fungicide_desc" => query.OrderByDescending(f => f.Fungicides.Name_Fungicide),
                _ => query.OrderBy(f => f.Pests.Pest),
            };

            var model = await query.ToListAsync();
            return View(model);
        }

        // GET: Pest_X_Fungicide/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pest_x_fungicide = await _context.Pest_X_Fungicide
                .FirstOrDefaultAsync(m => m.Id_Pest_X_Fungicide == id);
            if (pest_x_fungicide == null)
            {
                return NotFound();
            }

            return View(pest_x_fungicide);
        }

        // GET: Pest_X_Fungicide/Create
        public async Task<IActionResult> Create()
        {
            var pests = await _context.Pests.ToListAsync();
            var fungicides = await _context.Fungicides.ToListAsync();

            if (pests == null || fungicides == null)
            {
                return NotFound();
            }

            ViewData["Id_Pest"] = new SelectList(pests, "Id_Pest", "Pest");
            ViewData["Id_Fungicide"] = new SelectList(fungicides, "Id_Fungicide", "Name_Fungicide");
            return View();
        }

        // POST: Pest_X_Fungicide/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Pest_X_Fungicide,Id_Pest,Id_Fungicide,Deleted")] Pest_X_Fungicide pest_x_fungicide)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pest_x_fungicide);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var pests = await _context.Pests.ToListAsync();
            var fungicides = await _context.Fungicides.ToListAsync();

            if (pests == null || fungicides == null)
            {
                return NotFound();
            }

            ViewData["Id_Pest"] = new SelectList(pests, "Id_Pest", "Pest", pest_x_fungicide.Id_Pest);
            ViewData["Id_Fungicide"] = new SelectList(fungicides, "Id_Fungicide", "Name_Fungicide", pest_x_fungicide.Id_Fungicide);
            return View(pest_x_fungicide);
        }

        // GET: Pest_X_Fungicide/Edit/5
        // GET: Pest_X_Fungicide/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pest_x_fungicide = await _context.Pest_X_Fungicide.FindAsync(id);
            if (pest_x_fungicide == null)
            {
                return NotFound();
            }

            ViewData["Id_Pest"] = new SelectList(_context.Pests, "Id_Pest", "Pest", pest_x_fungicide.Id_Pest);
            ViewData["Id_Fungicide"] = new SelectList(_context.Fungicides, "Id_Fungicide", "Name_Fungicide", pest_x_fungicide.Id_Fungicide);

            return View(pest_x_fungicide);
        }

        // POST: Pest_X_Fungicide/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Pest_X_Fungicide,Id_Pest,Id_Fungicide")] Pest_X_Fungicide pest_x_fungicide)
        {
            if (id != pest_x_fungicide.Id_Pest_X_Fungicide)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pest_x_fungicide);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Fungicide_X_Pompon_PartExists(pest_x_fungicide.Id_Pest_X_Fungicide))
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

            ViewData["Id_Pest"] = new SelectList(_context.Pests, "Id_Pest", "Pest", pest_x_fungicide.Id_Pest);
            ViewData["Id_Fungicide"] = new SelectList(_context.Fungicides, "Id_Fungicide", "Name_Fungicide", pest_x_fungicide.Id_Fungicide);

            return View(pest_x_fungicide);
        }

        // GET: Pest_X_Fungicide/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pest_x_fungicide = await _context.Pest_X_Fungicide
                .FirstOrDefaultAsync(m => m.Id_Pest_X_Fungicide == id);
            if (pest_x_fungicide == null)
            {
                return NotFound();
            }

            return View(pest_x_fungicide);
        }

        // POST: Pest_X_Fungicide/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pest_x_fungicide = await _context.Pest_X_Fungicide.FindAsync(id);
            if (pest_x_fungicide != null)
            {
                pest_x_fungicide.Deleted = true;
                _context.Pest_X_Fungicide.Update(pest_x_fungicide);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Fungicide_X_Pompon_PartExists(int id)
        {
            return _context.Pest_X_Fungicide.Any(e => e.Id_Pest_X_Fungicide == id);
        }
    }
}
