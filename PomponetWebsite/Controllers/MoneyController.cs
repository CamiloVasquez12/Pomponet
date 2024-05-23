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
    public class MoneyController : Controller
    {
        private readonly CropsDbContext _context;

        public MoneyController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: Moneys
        public async Task<IActionResult> Index(int? buscar, string filtro)
        {
            var money = from mone in _context.Money select mone;

            if (buscar.HasValue && buscar.Value != 0)  // Validación para evitar buscar con valor 0 o nulo
            {
                string buscarStr = buscar.Value.ToString();
                money = money.Where(s => s.Quantity.ToString().Contains(buscarStr));
            }

            ViewData["FiltroQuantity"] = String.IsNullOrEmpty(filtro) ? "QuantityDescendente" : "";
            ViewData["FiltroId_Player"] = filtro == "Id_PlayerAscendente" ? "Id_PlayerDescendente" : "Id_PlayerAscendente";

            switch (filtro)
            {
                case "QuantityDescendente":
                    money = money.OrderByDescending(mone => mone.Quantity);
                    break;
                case "Id_PlayerDescendente":
                    money = money.OrderByDescending(mone => mone.Id_Player);
                    break;
                case "Id_PlayerAscendente":
                    money = money.OrderBy(mone => mone.Id_Player);
                    break;
                default:
                    money = money.OrderBy(mone => mone.Quantity);
                    break;
            }

            return View(await money.ToListAsync());
        }

        // GET: Moneys/Details/5
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

        // GET: Moneys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Moneys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            return View(money);
        }

        // GET: Moneys/Edit/5
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
            return View(money);
        }

        // POST: Moneys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            return View(money);
        }

        // GET: Moneys/Delete/5
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

        // POST: Moneys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mone = await _context.Money.FindAsync(id);
            if (mone != null)
            {
                mone.Deleted = true; // Marca el registro como eliminado
                _context.Money.Update(mone); // Actualiza el registro en el contexto
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoneyExists(int id)
        {
            return _context.Money.Any(e => e.Id_Money == id);
        }
    }
}
