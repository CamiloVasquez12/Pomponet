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
            var types_fungicides = from type_fungicide in _context.Types_Fungicides select type_fungicide;
            if (!String.IsNullOrEmpty(buscar))
            {
                types_fungicides = types_fungicides.Where(s => s.Type_Fungicide!.Contains(buscar));
            }
            ViewData["FiltroType_Fungicide"] = String.IsNullOrEmpty(filtro) ? "Type_FungicideDescendente" : "";

            switch (filtro)
            {
                case "Type_FungicideDescendente":
                    types_fungicides = types_fungicides.OrderByDescending(types_fungicides => types_fungicides.Type_Fungicide);
                    break;
                default:
                    types_fungicides = types_fungicides.OrderBy(types_fungicides => types_fungicides.Type_Fungicide);
                    break;
            }
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
        public IActionResult Create()
        {
            ViewBag.Fungicides = new SelectList(_context.Fungicides, "Id_Fungicide", "Name_Fungicide");
            return View();
        }

        // POST: Types_Fungicides/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Type_Fungicide,Type_Fungicide,Id_Funicides,Deleted")] Types_Fungicides types_Fungicides)
        {
            if (ModelState.IsValid)
            {
                _context.Add(types_Fungicides);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return View(types_Fungicides);
        }

        // POST: Types_Fungicides/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // POST: Types_Fungicides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var types_Fungicides = await _context.Types_Fungicides.FindAsync(id);
            if (types_Fungicides != null)
            {
                _context.Types_Fungicides.Remove(types_Fungicides);
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
