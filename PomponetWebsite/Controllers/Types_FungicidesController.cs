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
    public class Types_FungicidesController : Controller
    {
        private readonly CropsDbContext _context;

        public Types_FungicidesController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: Types_Fungicides
        public async Task<IActionResult> Index(string buscar, string filtro)
        {
            IQueryable<Types_Fungicides> types_fungicides = _context.Types_Fungicides;

            if (!String.IsNullOrEmpty(buscar))
            {
                types_fungicides = types_fungicides.Where(s => s.Type_Fungicide.Contains(buscar));
            }

            ViewData["FiltroType_Fungicide"] = String.IsNullOrEmpty(filtro) ? "Type_FungicideDescendente" : "";
            ViewData["FiltroId_Funicides"] = filtro == "Id_FunicidesAscendente" ? "Id_FunicidesDescendente" : "Id_FunicidesAscendente";

            switch (filtro)
            {
                case "Type_FungicideDescendente":
                    types_fungicides = types_fungicides.OrderByDescending(tf => tf.Type_Fungicide);
                    break;
                case "Id_FunicidesDescendente":
                    types_fungicides = types_fungicides.OrderByDescending(tf => tf.Id_Funicides);
                    break;
                case "Id_FunicidesAscendente":
                    types_fungicides = types_fungicides.OrderBy(tf => tf.Id_Funicides);
                    break;
                default:
                    types_fungicides = types_fungicides.OrderBy(tf => tf.Type_Fungicide);
                    break;
            }

            types_fungicides = types_fungicides.Include(tf => tf.Fungicides);

            return View(await types_fungicides.ToListAsync());
        }



        // GET: Types_Fungicides/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var types_Fungicides = await _context.Types_Fungicides
                .FirstOrDefaultAsync(m => m.Id_Type_Fungicide == id);
            if (types_Fungicides == null)
            {
                return NotFound();
            }

            return View(types_Fungicides);
        }

        // GET: Types_Fungicides/Create
        // GET: Types_Fungicides/Create
        public IActionResult Create()
        {
            ViewBag.Fungicides = new SelectList(_context.Fungicides, "Id_Fungicide", "Name_Fungicide");
            return View();
        }

        // POST: Types_Fungicides/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type_Fungicide,Id_Funicides,Deleted")] Types_Fungicides types_Fungicides)
        {
            if (ModelState.IsValid)
            {
                _context.Add(types_Fungicides);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Fungicides = new SelectList(_context.Fungicides, "Id_Fungicide", "Name_Fungicide", types_Fungicides.Id_Funicides);
            return View(types_Fungicides);
        }


        // GET: Types_Fungicides/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var types_Fungicides = await _context.Types_Fungicides.FindAsync(id);
            if (types_Fungicides == null)
            {
                return NotFound();
            }
            ViewBag.Fungicides = new SelectList(_context.Fungicides, "Id_Fungicide", "Name_Fungicide", types_Fungicides.Id_Funicides);
            return View(types_Fungicides);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Type_Fungicide,Type_Fungicide,Id_Funicides,Deleted")] Types_Fungicides types_Fungicides)
        {
            if (id != types_Fungicides.Id_Type_Fungicide)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(types_Fungicides);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Types_FungicidesExists(types_Fungicides.Id_Type_Fungicide))
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
            return View(types_Fungicides);
        }

        // GET: Types_Fungicides/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var types_Fungicides = await _context.Types_Fungicides
                .FirstOrDefaultAsync(m => m.Id_Type_Fungicide == id);
            if (types_Fungicides == null)
            {
                return NotFound();
            }

            return View(types_Fungicides);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var types_fungicides = await _context.Types_Fungicides.FindAsync(id);
            if (types_fungicides != null)
            {
                types_fungicides.Deleted = true;
                _context.Types_Fungicides.Update(types_fungicides);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Types_FungicidesExists(int id)
        {
            return _context.Types_Fungicides.Any(e => e.Id_Type_Fungicide == id);
        }
    }
}
