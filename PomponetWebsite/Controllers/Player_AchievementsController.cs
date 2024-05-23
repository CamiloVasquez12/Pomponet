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
    public class Player_AchievementsController : Controller
    {
        private readonly CropsDbContext _context;

        public Player_AchievementsController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: Player_Achievements
        public async Task<IActionResult> Index(int? buscar, string filtro)
        {
            var player_achievements = from player_achievement in _context.Player_Achievements select player_achievement;

            if (buscar.HasValue && buscar.Value != 0)  // Validación para evitar buscar con valor 0 o nulo
            {
                string buscarStr = buscar.Value.ToString();
                player_achievements = player_achievements.Where(s => s.Id_Achievement.ToString().Contains(buscarStr));
            }

            ViewData["FiltroId_Achievement"] = String.IsNullOrEmpty(filtro) ? "Id_AchievementDescendente" : "";
            ViewData["FiltroLogros_Totales"] = filtro == "Logros_TotalesAscendente" ? "Logros_TotalesDescendente" : "Logros_TotalesAscendente";

            switch (filtro)
            {
                case "Id_AchievementDescendente":
                    player_achievements = player_achievements.OrderByDescending(player_achievement => player_achievement.Id_Achievement);
                    break;
                case "Logros_TotalesDescendente":
                    player_achievements = player_achievements.OrderByDescending(player_achievement => player_achievement.Logros_Totales);
                    break;
                case "Logros_TotalesAscendente":
                    player_achievements = player_achievements.OrderBy(player_achievement => player_achievement.Logros_Totales);
                    break;
                default:
                    player_achievements = player_achievements.OrderBy(player_achievement => player_achievement.Id_Achievement);
                    break;
            }

            return View(await player_achievements.ToListAsync());
        }

        // GET: Player_Achievements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player_Achievements = await _context.Player_Achievements
                .FirstOrDefaultAsync(m => m.Id_Player_Achievement == id);
            if (player_Achievements == null)
            {
                return NotFound();
            }

            return View(player_Achievements);
        }

        // GET: Player_Achievements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Player_Achievements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Player_Achievement,Id_Achievement,Id_Player,Logros_Totales,Deleted")] Player_Achievements player_Achievements)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player_Achievements);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(player_Achievements);
        }

        // GET: Player_Achievements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player_Achievements = await _context.Player_Achievements.FindAsync(id);
            if (player_Achievements == null)
            {
                return NotFound();
            }
            return View(player_Achievements);
        }

        // POST: Player_Achievements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Player_Achievement,Id_Achievement,Logros_Totales,Id_Player,Deleted")] Player_Achievements player_Achievements)
        {
            if (id != player_Achievements.Id_Player_Achievement)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player_Achievements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Player_AchievementsExists(player_Achievements.Id_Player_Achievement))
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
            return View(player_Achievements);
        }

        // GET: Player_Achievements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player_Achievements = await _context.Player_Achievements
                .FirstOrDefaultAsync(m => m.Id_Player_Achievement == id);
            if (player_Achievements == null)
            {
                return NotFound();
            }

            return View(player_Achievements);
        }

        // POST: Player_Achievements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player_achievement = await _context.Player_Achievements.FindAsync(id);
            if (player_achievement != null)
            {
                player_achievement.Deleted = true; // Marca el registro como eliminado
                _context.Player_Achievements.Update(player_achievement); // Actualiza el registro en el contexto
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Player_AchievementsExists(int id)
        {
            return _context.Player_Achievements.Any(e => e.Id_Player_Achievement == id);
        }
    }
}
