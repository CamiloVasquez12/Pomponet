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
    public class SensorsController : Controller
    {
        private readonly CropsDbContext _context;

        public SensorsController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: Sensors
        public async Task<IActionResult> Index(string buscar, string filtro)
        {
            var sensors = from sensor in _context.Sensors select sensor;
            if (!String.IsNullOrEmpty(buscar))
            {
                sensors = sensors.Where(s => s.Sensor!.Contains(buscar));
            }
            ViewData["FiltroSensor"] = String.IsNullOrEmpty(filtro) ? "SensorDescendente" : "";
            ViewData["FiltroPrice"] = filtro == "PriceAscendente" ? "PriceDescendente" : "PriceAscendente";

            switch (filtro)
            {
                case "SensorDescendente":
                    sensors = sensors.OrderByDescending(sensors => sensors.Sensor);
                    break;
                case "PriceDescendente":
                    sensors = sensors.OrderByDescending(sensors => sensors.Price);
                    break;
                case "PriceAscendente":
                    sensors = sensors.OrderBy(sensors => sensors.Price);
                    break;
                default:
                    sensors = sensors.OrderBy(sensors => sensors.Sensor);
                    break;
            }
            return View(await sensors.ToListAsync());
        }

        // GET: Sensors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensors = await _context.Sensors
                .FirstOrDefaultAsync(m => m.Id_Sensor == id);
            if (sensors == null)
            {
                return NotFound();
            }

            return View(sensors);
        }

        // GET: Sensors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sensors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Sensor,Sensor,Price,Description,Id_crop,Deleted")] Sensors sensors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sensors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sensors);
        }

        // GET: Sensors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensors = await _context.Sensors.FindAsync(id);
            if (sensors == null)
            {
                return NotFound();
            }
            return View(sensors);
        }

        // POST: Sensors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Sensor,Sensor,Price,Description,Id_crop,Deleted")] Sensors sensors)
        {
            if (id != sensors.Id_Sensor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sensors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SensorsExists(sensors.Id_Sensor))
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
            return View(sensors);
        }

        // GET: Sensors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensors = await _context.Sensors
                .FirstOrDefaultAsync(m => m.Id_Sensor == id);
            if (sensors == null)
            {
                return NotFound();
            }

            return View(sensors);
        }

        // POST: Sensors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sensors = await _context.Sensors.FindAsync(id);
            if (sensors != null)
            {
                sensors.Deleted = true; // Marca el registro como eliminado
                _context.Sensors.Update(sensors); // Actualiza el registro en el contexto
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SensorsExists(int id)
        {
            return _context.Sensors.Any(e => e.Id_Sensor == id);
        }
    }
}
