using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["AchievementSortParm"] = sortOrder == "achievement" ? "achievement_desc" : "achievement";
            ViewData["PlayerSortParm"] = sortOrder == "player" ? "player_desc" : "player";

            var query = _context.Player_Achievements
                                .Include(f => f.Achievements)
                                .Include(f => f.Players)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(f => f.Achievements.Achievement.Contains(searchString) ||
                                         f.Players.Score.ToString().Contains(searchString));
            }

            query = sortOrder switch
            {
                "achievement_desc" => query.OrderByDescending(f => f.Achievements.Achievement),
                "player_desc" => query.OrderByDescending(f => f.Players.Score),
                "achievement" => query.OrderBy(f => f.Achievements.Achievement),
                "player" => query.OrderBy(f => f.Players.Score),
                _ => query.OrderBy(f => f.Achievements.Achievement)
            };

            var model = await query.ToListAsync();
            return View(model);
        }

        // GET: Player_Achievements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player_achievements = await _context.Player_Achievements
                .Include(p => p.Achievements)
                .Include(p => p.Players)
                .FirstOrDefaultAsync(m => m.Id_Player_Achievement == id);
            if (player_achievements == null)
            {
                return NotFound();
            }

            return View(player_achievements);
        }

        // GET: Player_Achievements/Create
        public async Task<IActionResult> Create()
        {
            var achievements = await _context.Achievements.ToListAsync();
            var players = await _context.Players.ToListAsync();

            ViewData["Id_Achievement"] = new SelectList(achievements, "Id_Achievement", "Achievement");
            ViewData["Id_Player"] = new SelectList(players, "Id_Player", "Score");
            return View();
        }

        // POST: Player_Achievements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Player_Achievement,Id_Achievement,Logros_Totales,Id_Player,Deleted")] Player_Achievements player_achievements)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player_achievements);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var achievements = await _context.Achievements.ToListAsync();
            var players = await _context.Players.ToListAsync();
            ViewData["Id_Achievement"] = new SelectList(achievements, "Id_Achievement", "Achievement", player_achievements.Id_Achievement);
            ViewData["Id_Player"] = new SelectList(players, "Id_Player", "Score", player_achievements.Id_Player);
            return View(player_achievements);
        }

        // GET: Player_Achievements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player_achievements = await _context.Player_Achievements.FindAsync(id);
            if (player_achievements == null)
            {
                return NotFound();
            }
            ViewData["Id_Achievement"] = new SelectList(_context.Achievements, "Id_Achievement", "Achievement", player_achievements.Id_Achievement);
            ViewData["Id_Player"] = new SelectList(_context.Players, "Id_Player", "Score", player_achievements.Id_Player);
            return View(player_achievements);
        }

        // POST: Player_Achievements/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Player_Achievement,Id_Achievement,Logros_Totales,Id_Player,Deleted")] Player_Achievements player_achievements)
        {
            if (id != player_achievements.Id_Player_Achievement)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player_achievements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Player_AchievementsExists(player_achievements.Id_Player_Achievement))
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
            ViewData["Id_Achievement"] = new SelectList(_context.Achievements, "Id_Achievement", "Achievement", player_achievements.Id_Achievement);
            ViewData["Id_Player"] = new SelectList(_context.Players, "Id_Player", "Score", player_achievements.Id_Player);
            return View(player_achievements);
        }

        private bool Player_AchievementsExists(int id)
        {
            return _context.Player_Achievements.Any(e => e.Id_Player_Achievement == id);
        }

        // GET: Player_Achievements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player_achievements = await _context.Player_Achievements
                .Include(p => p.Achievements)
                .Include(p => p.Players)
                .FirstOrDefaultAsync(m => m.Id_Player_Achievement == id);
            if (player_achievements == null)
            {
                return NotFound();
            }

            return View(player_achievements);
        }

        // POST: Player_Achievements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player_achievements = await _context.Player_Achievements.FindAsync(id);
            if (player_achievements != null)
            {
                player_achievements.Deleted = true;
                _context.Player_Achievements.Update(player_achievements);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
