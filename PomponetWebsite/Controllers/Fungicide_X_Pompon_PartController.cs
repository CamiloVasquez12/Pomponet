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
    public class Fungicide_X_Pompon_PartController : Controller
    {
        private readonly CropsDbContext _context;

        public Fungicide_X_Pompon_PartController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: Fungicide_X_Pompon_Part
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["PartSortParm"] = string.IsNullOrEmpty(sortOrder) ? "part_desc" : "";
            ViewData["FungicideSortParm"] = sortOrder == "fungicide" ? "fungicide_desc" : "fungicide";

            var query = _context.Fungicide_X_Pompon_Part
                                .Include(f => f.Pompon_Parts)
                                .Include(f => f.Fungicides)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(f => f.Pompon_Parts.Part.Contains(searchString) ||
                                         f.Fungicides.Name_Fungicide.Contains(searchString));
            }

            query = sortOrder switch
            {
                "part_desc" => query.OrderByDescending(f => f.Pompon_Parts.Part),
                "fungicide" => query.OrderBy(f => f.Fungicides.Name_Fungicide),
                "fungicide_desc" => query.OrderByDescending(f => f.Fungicides.Name_Fungicide),
                _ => query.OrderBy(f => f.Pompon_Parts.Part),
            };

            var model = await query.ToListAsync();
            return View(model);
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
        public async Task<IActionResult> Create()
        {
            var pomponParts = await _context.Pompon_Parts.ToListAsync();
            var fungicides = await _context.Fungicides.ToListAsync();

            if (pomponParts == null || fungicides == null)
            {
                return NotFound();
            }

            ViewData["Id_Pompon_Part"] = new SelectList(pomponParts, "Id_Pompon_Part", "Part");
            ViewData["Id_Fungicide"] = new SelectList(fungicides, "Id_Fungicide", "Name_Fungicide");
            return View();
        }

        // POST: Fungicide_X_Pompon_Part/Create
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

            var pomponParts = await _context.Pompon_Parts.ToListAsync();
            var fungicides = await _context.Fungicides.ToListAsync();

            if (pomponParts == null || fungicides == null)
            {
                return NotFound();
            }

            ViewData["Id_Pompon_Part"] = new SelectList(pomponParts, "Id_Pompon_Part", "Part", fungicide_X_Pompon_Part.Id_Pompon_Part);
            ViewData["Id_Fungicide"] = new SelectList(fungicides, "Id_Fungicide", "Name_Fungicide", fungicide_X_Pompon_Part.Id_Fungicide);
            return View(fungicide_X_Pompon_Part);
        }

        // GET: Fungicide_X_Pompon_Part/Edit/5
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

            ViewData["Id_Pompon_Part"] = new SelectList(_context.Pompon_Parts, "Id_Pompon_Part", "Part", fungicide_X_Pompon_Part.Id_Pompon_Part);
            ViewData["Id_Fungicide"] = new SelectList(_context.Fungicides, "Id_Fungicide", "Name_Fungicide", fungicide_X_Pompon_Part.Id_Fungicide);

            return View(fungicide_X_Pompon_Part);
        }

        // POST: Fungicide_X_Pompon_Part/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Fungicide_X_Pompon_Part,Id_Pompon_Part,Id_Fungicide")] Fungicide_X_Pompon_Part fungicide_X_Pompon_Part)
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

            ViewData["Id_Pompon_Part"] = new SelectList(_context.Pompon_Parts, "Id_Pompon_Part", "Part", fungicide_X_Pompon_Part.Id_Pompon_Part);
            ViewData["Id_Fungicide"] = new SelectList(_context.Fungicides, "Id_Fungicide", "Name_Fungicide", fungicide_X_Pompon_Part.Id_Fungicide);

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
                fungicide_x_pompon_par.Deleted = true;
                _context.Fungicide_X_Pompon_Part.Update(fungicide_x_pompon_par);
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
