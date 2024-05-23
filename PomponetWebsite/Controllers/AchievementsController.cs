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
    public class AchievementsController : Controller
    {
        private readonly CropsDbContext _context;

        public AchievementsController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: Achievements
        public async Task<IActionResult> Index(string buscar, string filtro)
        {
            var achievements = from achievement in _context.Achievements select achievement;
            if (!String.IsNullOrEmpty(buscar))
            {
                achievements = achievements.Where(s => s.Achievement!.Contains(buscar));
            }
            ViewData["FiltroAchievement"] = String.IsNullOrEmpty(filtro) ? "AchievementDescendente" : "";

            switch (filtro)
            {
                case "AchievementDescendente":
                    achievements = achievements.OrderByDescending(achievements => achievements.Achievement);
                    break;
                default:
                    achievements = achievements.OrderBy(achievements => achievements.Achievement);
                    break;
            }
            return View(await achievements.ToListAsync());
        }

        // GET: Achievements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievements = await _context.Achievements
                .FirstOrDefaultAsync(m => m.Id_Achievement == id);
            if (achievements == null)
            {
                return NotFound();
            }

            return View(achievements);
        }

        // GET: Achievements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Achievements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Achievement,Achievement,Deleted")] Achievements achievements)
        {
            if (ModelState.IsValid)
            {
                _context.Add(achievements);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(achievements);
        }

        // GET: Achievements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievements = await _context.Achievements.FindAsync(id);
            if (achievements == null)
            {
                return NotFound();
            }
            return View(achievements);
        }

        // POST: Achievements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Achievement,Achievement,Deleted")] Achievements achievements)
        {
            if (id != achievements.Id_Achievement)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(achievements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AchievementsExists(achievements.Id_Achievement))
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
            return View(achievements);
        }

        // GET: Achievements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievements = await _context.Achievements
                .FirstOrDefaultAsync(m => m.Id_Achievement == id);
            if (achievements == null)
            {
                return NotFound();
            }

            return View(achievements);
        }

        // POST: Achievements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var achievements = await _context.Achievements.FindAsync(id);
            if (achievements != null)
            {
                achievements.Deleted = true; // Marca el registro como eliminado
                _context.Achievements.Update(achievements); // Actualiza el registro en el contexto
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AchievementsExists(int id)
        {
            return _context.Achievements.Any(e => e.Id_Achievement == id);
        }
    }
}
