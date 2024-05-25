using System;
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
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["CropSortParm"] = string.IsNullOrEmpty(sortOrder) ? "crop_desc" : "crop_asc";
            ViewData["SensorSortParm"] = sortOrder == "sensor_asc" ? "sensor_desc" : "sensor_asc";

            var query = _context.Sensors.Include(s => s.Crops).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Sensor.Contains(searchString) || s.Crops.Crop_Number.ToString().Contains(searchString));
            }

            query = sortOrder switch
            {
                "crop_desc" => query.OrderByDescending(s => s.Crops.Crop_Number),
                "sensor_asc" => query.OrderBy(s => s.Sensor),
                "sensor_desc" => query.OrderByDescending(s => s.Sensor),
                _ => query.OrderBy(s => s.Crops.Crop_Number),
            };

            return View(await query.ToListAsync());
        }

        // GET: Sensors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.Sensors.Include(s => s.Crops).FirstOrDefaultAsync(m => m.Id_Sensor == id);
            if (sensor == null)
            {
                return NotFound();
            }

            return View(sensor);
        }

        // GET: Sensors/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Id_crop"] = new SelectList(await _context.Crop.ToListAsync(), "Id_Crop", "Crop_Number");
            return View();
        }

        // POST: Sensors/Create
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
            ViewData["Id_crop"] = new SelectList(await _context.Crop.ToListAsync(), "Id_Crop", "Crop_Number", sensors.Id_crop);
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
            ViewData["Id_crop"] = new SelectList(await _context.Crop.ToListAsync(), "Id_Crop", "Crop_Number", sensors.Id_crop);
            return View(sensors);
        }

        // POST: Sensors/Edit/5
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
            ViewData["Id_crop"] = new SelectList(await _context.Crop.ToListAsync(), "Id_Crop", "Crop_Number", sensors.Id_crop);
            return View(sensors);
        }

        // GET: Sensors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensors = await _context.Sensors.Include(s => s.Crops).FirstOrDefaultAsync(m => m.Id_Sensor == id);
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
                sensors.Deleted = true;
                _context.Sensors.Update(sensors);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SensorsExists(int id)
        {
            return _context.Sensors.Any(e => e.Id_Sensor == id);
        }
    }
}
