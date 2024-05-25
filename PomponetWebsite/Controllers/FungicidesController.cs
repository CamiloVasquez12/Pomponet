using System;
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
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["QuantitySortParm"] = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";
            ViewData["CurrentFilter"] = searchString;

            var fungicides = from f in _context.Fungicides.Include(f => f.Crops)
                             select f;

            if (!String.IsNullOrEmpty(searchString))
            {
                fungicides = fungicides.Where(s => s.Name_Fungicide.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    fungicides = fungicides.OrderByDescending(s => s.Name_Fungicide);
                    break;
                case "Quantity":
                    fungicides = fungicides.OrderBy(s => s.Quantity);
                    break;
                case "quantity_desc":
                    fungicides = fungicides.OrderByDescending(s => s.Quantity);
                    break;
                default:
                    fungicides = fungicides.OrderBy(s => s.Name_Fungicide);
                    break;
            }

            return View(await fungicides.AsNoTracking().ToListAsync());
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
        public async Task<IActionResult> Create()
        {
            var crops = await _context.Crop.ToListAsync();
            if (crops == null || !crops.Any())
            {
                return NotFound();
            }

            ViewData["Id_crop"] = new SelectList(crops, "Id_Crop", "Crop_Number");
            return View();
        }

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

            var crops = await _context.Crop.ToListAsync();
            if (crops == null || !crops.Any())
            {
                return NotFound();
            }

            ViewData["Id_crop"] = new SelectList(crops, "Id_Crop", "Crop_Number", fungicides.Id_crop);
            return View(fungicides);
        }

        // GET: Fungicides/Edit/12
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fungicide = await _context.Fungicides.FindAsync(id);
            if (fungicide == null)
            {
                return NotFound();
            }

            // Log
            Console.WriteLine($"Editing fungicide with ID: {id}");
            Console.WriteLine($"Name: {fungicide.Name_Fungicide}");
            Console.WriteLine($"Quantity: {fungicide.Quantity}");
            Console.WriteLine($"Description: {fungicide.Description}");
            Console.WriteLine($"Crop ID: {fungicide.Id_crop}");

            ViewData["Id_crop"] = new SelectList(_context.Crop, "Id_Crop", "Crop_Number", fungicide.Id_crop);
            return View(fungicide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Fungicide,Name_Fungicide,Quantity,Description,Id_crop")] Fungicides fungicides)
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
            ViewData["Id_crop"] = new SelectList(_context.Crop, "Id_Crop", "Crop_Number", fungicides.Id_crop);
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
