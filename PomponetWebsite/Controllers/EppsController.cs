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
    public class EppsController : Controller
    {
        private readonly CropsDbContext _context;

        public EppsController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: Epps
        public async Task<IActionResult> Index(string buscar, string filtro)
        {
            var epps = from epp in _context.Epps select epp;
            if (!String.IsNullOrEmpty(buscar))
            {
                epps = epps.Where(s => s.Name_Epp!.Contains(buscar));
            }
            ViewData["FiltroName_Epp"] = String.IsNullOrEmpty(filtro) ? "Name_EppDescendente" : "";

            switch (filtro)
            {
                case "Name_EppDescendente":
                    epps = epps.OrderByDescending(epps => epps.Name_Epp);
                    break;
                default:
                    epps = epps.OrderBy(epps => epps.Name_Epp);
                    break;
            }
            return View(await epps.ToListAsync());
        }

        // GET: Epps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epps = await _context.Epps
                .FirstOrDefaultAsync(m => m.Id_Epp == id);
            if (epps == null)
            {
                return NotFound();
            }

            return View(epps);
        }

        // GET: Epps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Epps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Epp,Name_Epp,Deleted")] Epps epps)
        {
            if (ModelState.IsValid)
            {
                _context.Add(epps);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(epps);
        }

        // GET: Epps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epps = await _context.Epps.FindAsync(id);
            if (epps == null)
            {
                return NotFound();
            }
            return View(epps);
        }

        // POST: Epps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Epp,Name_Epp,Deleted")] Epps epps)
        {
            if (id != epps.Id_Epp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(epps);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EppsExists(epps.Id_Epp))
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
            return View(epps);
        }

        // GET: Epps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epps = await _context.Epps
                .FirstOrDefaultAsync(m => m.Id_Epp == id);
            if (epps == null)
            {
                return NotFound();
            }

            return View(epps);
        }

        // POST: Epps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var epps = await _context.Epps.FindAsync(id);
            if (epps != null)
            {
                epps.Deleted = true; // Marca el registro como eliminado
                _context.Epps.Update(epps); // Actualiza el registro en el contexto
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EppsExists(int id)
        {
            return _context.Epps.Any(e => e.Id_Epp == id);
        }
    }
}
