using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PomponetWebsite.Context;
using PomponetWebsite.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PomponetWebsite.Controllers
{
    public class Types_FungicidesController : Controller
    {
        private readonly CropsDbContext _context;

        public Types_FungicidesController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: Types_Fungicides
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["FungicideSortParm"] = string.IsNullOrEmpty(sortOrder) ? "fungicide_desc" : "fungicide_asc";
            ViewData["TypeSortParm"] = sortOrder == "type_asc" ? "type_desc" : "type_asc";

            var query = _context.Types_Fungicides
                                .Include(f => f.Fungicides)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(f => f.Fungicides.Name_Fungicide.Contains(searchString) || f.Type_Fungicide.Contains(searchString));
            }

            query = sortOrder switch
            {
                "fungicide_desc" => query.OrderByDescending(f => f.Fungicides.Name_Fungicide),
                "type_asc" => query.OrderBy(f => f.Type_Fungicide),
                "type_desc" => query.OrderByDescending(f => f.Type_Fungicide),
                _ => query.OrderBy(f => f.Fungicides.Name_Fungicide),
            };

            var model = await query.ToListAsync();
            return View(model);
        }

        // GET: Types_Fungicides/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var types_fungicide = await _context.Types_Fungicides
                .Include(t => t.Fungicides) // Include the related Fungicides entity
                .FirstOrDefaultAsync(m => m.Id_Type_Fungicide == id);
            if (types_fungicide == null)
            {
                return NotFound();
            }

            return View(types_fungicide);
        }

        // GET: TypesFungicides/Create
        public async Task<IActionResult> Create()
        {
            var fungicides = await _context.Fungicides.ToListAsync();
            ViewBag.Id_Funicides = new SelectList(fungicides, "Id_Fungicide", "Name_Fungicide");
            return View();
        }

        // POST: TypesFungicides/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Type_Fungicide,Type_Fungicide,Id_Funicides,Deleted")] Types_Fungicides typesFungicides)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typesFungicides);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var fungicides = await _context.Fungicides.ToListAsync();
            ViewBag.Id_Funicides = new SelectList(fungicides, "Id_Fungicide", "Name_Fungicide", typesFungicides.Id_Funicides);
            return View(typesFungicides);
        }

        // GET: Types_Fungicides/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var types_fungicide = await _context.Types_Fungicides.FindAsync(id);
            if (types_fungicide == null)
            {
                return NotFound();
            }

            var fungicides = await _context.Fungicides.ToListAsync();
            ViewBag.Id_Funicides = new SelectList(fungicides, "Id_Fungicide", "Name_Fungicide", types_fungicide.Id_Funicides);
            return View(types_fungicide);
        }

        // POST: Types_Fungicides/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Type_Fungicide,Type_Fungicide,Id_Funicides,Deleted")] Types_Fungicides types_fungicide)
        {
            if (id != types_fungicide.Id_Type_Fungicide)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(types_fungicide);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypesFungicidesExists(types_fungicide.Id_Type_Fungicide))
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

            var fungicides = await _context.Fungicides.ToListAsync();
            ViewBag.Id_Funicides = new SelectList(fungicides, "Id_Fungicide", "Name_Fungicide", types_fungicide.Id_Funicides);
            return View(types_fungicide);
        }

        // GET: Types_Fungicides/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var types_fungicide = await _context.Types_Fungicides
                .Include(t => t.Fungicides) // Include the related Fungicides entity
                .FirstOrDefaultAsync(m => m.Id_Type_Fungicide == id);
            if (types_fungicide == null)
            {
                return NotFound();
            }

            return View(types_fungicide);
        }

        // POST: Types_Fungicides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var types_fungicide = await _context.Types_Fungicides.FindAsync(id);
            if (types_fungicide != null)
            {
                types_fungicide.Deleted = true;
                _context.Types_Fungicides.Update(types_fungicide);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypesFungicidesExists(int id)
        {
            return _context.Types_Fungicides.Any(e => e.Id_Type_Fungicide == id);
        }
    }
}
