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
    public class MoneyController : Controller
    {
        private readonly CropsDbContext _context;

        public MoneyController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: Money
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["PlayerSortParm"] = string.IsNullOrEmpty(sortOrder) ? "player_desc" : "";
            ViewData["QuantitySortParm"] = sortOrder == "quantity" ? "quantity_desc" : "quantity";

            var query = _context.Money
                                .Include(f => f.Players)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int searchScore))
                {
                    query = query.Where(f => f.Players.Score == searchScore);
                }
            }

            query = sortOrder switch
            {
                "player_desc" => query.OrderByDescending(f => f.Players.Score),
                "quantity_desc" => query.OrderByDescending(f => f.Quantity),
                "quantity" => query.OrderBy(f => f.Quantity),
                _ => query.OrderBy(f => f.Players.Score)
            };

            var model = await query.ToListAsync();
            return View(model);
        }

        // GET: Money/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var money = await _context.Money
                .FirstOrDefaultAsync(m => m.Id_Money == id);
            if (money == null)
            {
                return NotFound();
            }

            return View(money);
        }

        // GET: Money/Create
        // GET: Money/Create
        public async Task<IActionResult> Create()
        {
            var players = await _context.Players.ToListAsync();

            if (players == null)
            {
                return NotFound();
            }

            ViewData["Id_Player"] = new SelectList(players, "Id_Player", "Score");
            return View();
        }

        // POST: Money/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Money,Quantity,Id_Player,Deleted")] Money money)
        {
            if (ModelState.IsValid)
            {
                _context.Add(money);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var players = await _context.Players.ToListAsync();

            if (players == null)
            {
                return NotFound();
            }

            ViewData["Id_Player"] = new SelectList(players, "Id_Player", "Score", money.Id_Player);
            return View(money);
        }

        // GET: Money/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var money = await _context.Money.FindAsync(id);
            if (money == null)
            {
                return NotFound();
            }

            ViewData["Id_Player"] = new SelectList(_context.Players, "Id_Player", "Score", money.Id_Player);
            return View(money);
        }

        // POST: Money/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Money,Quantity,Id_Player,Deleted")] Money money)
        {
            if (id != money.Id_Money)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(money);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoneyExists(money.Id_Money))
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

            ViewData["Id_Player"] = new SelectList(_context.Players, "Id_Player", "Score", money.Id_Player);
            return View(money);
        }

        private bool MoneyExists(int id)
        {
            return _context.Money.Any(e => e.Id_Money == id);
        }

        // GET: Money/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var money = await _context.Money
                .FirstOrDefaultAsync(m => m.Id_Money == id);
            if (money == null)
            {
                return NotFound();
            }

            return View(money);
        }

        // POST: Money/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var money = await _context.Money.FindAsync(id);
            if (money != null)
            {
                money.Deleted = true;
                _context.Money.Update(money);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Fungicide_X_Pompon_PartExists(int id)
        {
            return _context.Money.Any(e => e.Id_Money == id);
        }
    }
}
