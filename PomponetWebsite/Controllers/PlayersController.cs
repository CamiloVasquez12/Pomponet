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
    public class PlayersController : Controller
    {
        private readonly CropsDbContext _context;

        public PlayersController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: Players
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["ScoreSortParm"] = string.IsNullOrEmpty(sortOrder) ? "score_desc" : "";
            ViewData["PersonSortParm"] = sortOrder == "person" ? "person_desc" : "person";

            var query = _context.Players
                                .Include(f => f.People)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(f => f.People.Names.Contains(searchString));
            }

            query = sortOrder switch
            {
                "person_desc" => query.OrderByDescending(f => f.People.Names),
                "score" => query.OrderBy(f => f.Score),
                "score_desc" => query.OrderByDescending(f => f.Score),
                _ => query.OrderBy(f => f.People.Names),
            };

            var model = await query.ToListAsync();
            return View(model);
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Players
                .FirstOrDefaultAsync(m => m.Id_Player == id);
            if (players == null)
            {
                return NotFound();
            }

            return View(players);
        }

        // GET: Players/Create
        public async Task<IActionResult> Create()
        {
            var people = await _context.People.ToListAsync();
            if (people == null)
            {
                return NotFound();
            }

            ViewData["Id_Person"] = new SelectList(people, "Id_Person", "Names");
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Player,Score,Id_Person,Deleted")] Players players)
        {
            if (ModelState.IsValid)
            {
                _context.Add(players);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var people = await _context.People.ToListAsync();
            if (people == null)
            {
                return NotFound();
            }

            ViewData["Id_Person"] = new SelectList(people, "Id_Person", "Names", players.Id_Person);
            return View(players);
        }

        // GET: Players/Edit/5
        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Players.FindAsync(id);
            if (players == null)
            {
                return NotFound();
            }

            ViewData["Id_Person"] = new SelectList(_context.People, "Id_Person", "Names", players.Id_Person);
            return View(players);
        }

        // POST: Players/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Player,Score,Id_Person,Deleted")] Players players)
        {
            if (id != players.Id_Player)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(players);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayersExists(players.Id_Player))
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

            ViewData["Id_Person"] = new SelectList(_context.People, "Id_Person", "Names", players.Id_Person);
            return View(players);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Players
                .FirstOrDefaultAsync(m => m.Id_Player == id);
            if (players == null)
            {
                return NotFound();
            }

            return View(players);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var players = await _context.Players.FindAsync(id);
            if (players != null)
            {
                players.Deleted = true;
                _context.Players.Update(players);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayersExists(int id)
        {
            return _context.Players.Any(e => e.Id_Player == id);
        }
    }
}
