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
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["PersonSortParm"] = string.IsNullOrEmpty(sortOrder) ? "person_desc" : "";
            ViewData["ToolSortParm"] = sortOrder == "tool" ? "tool_desc" : "tool";
            ViewData["EppSortParm"] = sortOrder == "epp" ? "epp_desc" : "epp";
            ViewData["NumberInventorieSortParm"] = sortOrder == "number_inventorie" ? "number_inventorie_desc" : "number_inventorie";

            var query = _context.Inventories
                                .Include(f => f.People)
                                .Include(f => f.Aplication_Tools)
                                .Include(f => f.Epps)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(f => f.People.Names.Contains(searchString) ||
                                         f.Aplication_Tools.Tool.Contains(searchString) ||
                                         f.Epps.Name_Epp.Contains(searchString));
            }

            query = sortOrder switch
            {
                "person_desc" => query.OrderByDescending(f => f.People.Names),
                "tool" => query.OrderBy(f => f.Aplication_Tools.Tool),
                "tool_desc" => query.OrderByDescending(f => f.Aplication_Tools.Tool),
                "epp" => query.OrderBy(f => f.Epps.Name_Epp),
                "epp_desc" => query.OrderByDescending(f => f.Epps.Name_Epp),
                "number_inventorie" => query.OrderBy(f => f.Number_Inventorie),
                "number_inventorie_desc" => query.OrderByDescending(f => f.Number_Inventorie),
                _ => query.OrderBy(f => f.People.Names),
            };

            var model = await query.ToListAsync();
            return View(model);
        }

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventories = await _context.Inventories
                                            .Include(f => f.People)
                                            .Include(f => f.Aplication_Tools)
                                            .Include(f => f.Epps)
                                            .FirstOrDefaultAsync(m => m.Id_Inventory == id);
            if (inventories == null)
            {
                return NotFound();
            }

            return View(inventories);
        }

        // GET: Inventories/Create
        // GET: Inventories/Create
        public async Task<IActionResult> Create()
        {
            var people = await _context.People.ToListAsync();
            var aplicationtools = await _context.AplicationTools.ToListAsync();
            var epps = await _context.Epps.ToListAsync();

            if (people == null || aplicationtools == null || epps == null)
            {
                return NotFound();
            }

            ViewData["Id_Person"] = new SelectList(people, "Id_Person", "Names");
            ViewData["Id_AplicationTool"] = new SelectList(aplicationtools, "Id_AplicationTool", "Tool");
            ViewData["Id_Epp"] = new SelectList(epps, "Id_Epp", "Name_Epp");
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
            var people = await _context.People.ToListAsync();
            var aplicationtools = await _context.AplicationTools.ToListAsync();
            var epps = await _context.Epps.ToListAsync();

            if (people == null || aplicationtools == null || epps == null)
            {
                return NotFound();
            }

            ViewData["Id_Person"] = new SelectList(people, "Id_Person", "Names", inventories.Id_Person);
            ViewData["Id_AplicationTool"] = new SelectList(aplicationtools, "Id_AplicationTool", "Tool", inventories.Id_Tool);
            ViewData["Id_Epp"] = new SelectList(epps, "Id_Epp", "Name_Epp", inventories.Id_Epp);
            return View(inventories);
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventories = await _context.Inventories
                                            .Include(f => f.People)
                                            .Include(f => f.Aplication_Tools)
                                            .Include(f => f.Epps)
                                            .FirstOrDefaultAsync(m => m.Id_Inventory == id);
            if (inventories == null)
            {
                return NotFound();
            }

            ViewData["Id_Person"] = new SelectList(_context.People, "Id_Person", "Names", inventories.Id_Person);
            ViewData["Id_AplicationTool"] = new SelectList(_context.AplicationTools, "Id_AplicationTool", "Tool", inventories.Id_Tool);
            ViewData["Id_Epp"] = new SelectList(_context.Epps, "Id_Epp", "Name_Epp", inventories.Id_Epp);
            return View(inventories);
        }

        // POST: Inventories/Edit/5
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

            var people = await _context.People.ToListAsync();
            var aplicationTools = await _context.AplicationTools.ToListAsync();
            var epps = await _context.Epps.ToListAsync();

            ViewData["Id_Person"] = new SelectList(people, "Id_Person", "Names", inventories.Id_Person);
            ViewData["Id_AplicationTool"] = new SelectList(aplicationTools, "Id_AplicationTool", "Tool", inventories.Id_Tool);
            ViewData["Id_Epp"] = new SelectList(epps, "Id_Epp", "Name_Epp", inventories.Id_Epp);

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
                                            .Include(f => f.People)
                                            .Include(f => f.Aplication_Tools)
                                            .Include(f => f.Epps)
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
