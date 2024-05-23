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
    public class InventoriesController : Controller
    {
        private readonly CropsDbContext _context;

        public InventoriesController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: Inventories
        public async Task<IActionResult> Index(int? buscar, string filtro)
        {
            var inventories = from inventorie in _context.Inventories select inventorie;

            if (buscar.HasValue && buscar.Value != 0)  // Validación para evitar buscar con valor 0 o nulo
            {
                string buscarStr = buscar.Value.ToString();
                inventories = inventories.Where(s => s.Number_Inventorie.ToString().Contains(buscarStr));
            }

            ViewData["FiltroNumber_Inventorie"] = String.IsNullOrEmpty(filtro) ? "Number_InventorieDescendente" : "";
            ViewData["FiltroId_Person"] = filtro == "Id_PersonAscendente" ? "Id_PersonDescendente" : "Id_PersonAscendente";

            switch (filtro)
            {
                case "Number_InventorieDescendente":
                    inventories = inventories.OrderByDescending(inventorie => inventorie.Number_Inventorie);
                    break;
                case "Id_PersonDescendente":
                    inventories = inventories.OrderByDescending(inventorie => inventorie.Id_Person);
                    break;
                case "Id_PersonAscendente":
                    inventories = inventories.OrderBy(inventorie => inventorie.Id_Person);
                    break;
                default:
                    inventories = inventories.OrderBy(inventorie => inventorie.Number_Inventorie);
                    break;
            }

            return View(await inventories.ToListAsync());
        }

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventories = await _context.Inventories
                .FirstOrDefaultAsync(m => m.Id_Inventory == id);
            if (inventories == null)
            {
                return NotFound();
            }

            return View(inventories);
        }

        // GET: Inventories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Inventory,Number_Inventorie,Id_Person,Id_Tool,Id_Epp,Deleted")] Inventories inventories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventories);
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventories = await _context.Inventories.FindAsync(id);
            if (inventories == null)
            {
                return NotFound();
            }
            return View(inventories);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Inventory,Number_Inventorie,Id_Person,Id_Tool,Id_Epp,Deleted")] Inventories inventories)
        {
            if (id != inventories.Id_Inventory)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoriesExists(inventories.Id_Inventory))
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
            return View(inventories);
        }

        // GET: Inventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventories = await _context.Inventories
                .FirstOrDefaultAsync(m => m.Id_Inventory == id);
            if (inventories == null)
            {
                return NotFound();
            }

            return View(inventories);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventorie = await _context.Inventories.FindAsync(id);
            if (inventorie != null)
            {
                inventorie.Deleted = true; // Marca el registro como eliminado
                _context.Inventories.Update(inventorie); // Actualiza el registro en el contexto
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool InventoriesExists(int id)
        {
            return _context.Inventories.Any(e => e.Id_Inventory == id);
        }
    }
}
