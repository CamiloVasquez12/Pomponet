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
    public class AplicationToolsController : Controller
    {
        private readonly CropsDbContext _context;

        public AplicationToolsController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: AplicationTools
        public async Task<IActionResult> Index(string buscar, string filtro)
        {
            var aplicationtools = from tool in _context.AplicationTools select tool;
            if (!String.IsNullOrEmpty(buscar))
            {
                aplicationtools = aplicationtools.Where(s => s.Tool!.Contains(buscar));
            }
            ViewData["FiltroTool"] = String.IsNullOrEmpty(filtro) ? "ToolDescendente" : "";
            ViewData["FiltroPrice"] = filtro == "PriceAscendente" ? "PriceDescendente" : "PriceAscendente";

            switch (filtro)
            {
                case "ToolDescendente":
                    aplicationtools = aplicationtools.OrderByDescending(aplicationtools => aplicationtools.Tool);
                    break;
                case "PriceDescendente":
                    aplicationtools = aplicationtools.OrderByDescending(aplicationtools => aplicationtools.Price);
                    break;
                case "PriceAscendente":
                    aplicationtools = aplicationtools.OrderBy(aplicationtools => aplicationtools.Price);
                    break;
                default:
                    aplicationtools = aplicationtools.OrderBy(aplicationtools => aplicationtools.Tool);
                    break;
            }
            return View(await aplicationtools.ToListAsync());
        }

        // GET: AplicationTools/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aplicationTools = await _context.AplicationTools
                .FirstOrDefaultAsync(m => m.Id_AplicationTool == id);
            if (aplicationTools == null)
            {
                return NotFound();
            }

            return View(aplicationTools);
        }

        // GET: AplicationTools/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AplicationTools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_AplicationTool,Tool,Quantity,Description,Price,Deleted")] AplicationTools aplicationTools)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aplicationTools);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aplicationTools);
        }

        // GET: AplicationTools/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aplicationTools = await _context.AplicationTools.FindAsync(id);
            if (aplicationTools == null)
            {
                return NotFound();
            }
            return View(aplicationTools);
        }

        // POST: AplicationTools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_AplicationTool,Tool,Quantity,Description,Price,Deleted")] AplicationTools aplicationTools)
        {
            if (id != aplicationTools.Id_AplicationTool)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aplicationTools);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AplicationToolsExists(aplicationTools.Id_AplicationTool))
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
            return View(aplicationTools);
        }

        // GET: AplicationTools/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aplicationTools = await _context.AplicationTools
                .FirstOrDefaultAsync(m => m.Id_AplicationTool == id);
            if (aplicationTools == null)
            {
                return NotFound();
            }

            return View(aplicationTools);
        }

        // POST: AplicationTools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aplicationtools = await _context.AplicationTools.FindAsync(id);
            if (aplicationtools != null)
            {
                aplicationtools.Deleted = true; // Marca el registro como eliminado
                _context.AplicationTools.Update(aplicationtools); // Actualiza el registro en el contexto
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AplicationToolsExists(int id)
        {
            return _context.AplicationTools.Any(e => e.Id_AplicationTool == id);
        }
    }
}
