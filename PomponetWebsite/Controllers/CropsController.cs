﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PomponetWebsite.Context;
using PomponetWebsite.Models;

namespace PomponetWebsite.Controllers
{
    public class CropsController : Controller
    {
        private readonly CropsDbContext _context;

        public CropsController(CropsDbContext context)
        {
            _context = context;
        }

        // GET: Crops
        public async Task<IActionResult> Index(int? buscar, string filtro)
        {
            var crops = from crop in _context.Crop select crop;

            if (buscar.HasValue && buscar.Value != 0)  // Validación para evitar buscar con valor 0 o nulo
            {
                string buscarStr = buscar.Value.ToString();
                crops = crops.Where(s => s.Crop_Number.ToString().Contains(buscarStr));
            }

            ViewData["FiltroCrop_Number"] = String.IsNullOrEmpty(filtro) ? "Crop_NumberDescendente" : "";
            ViewData["FiltroId_Player"] = filtro == "Id_PlayerAscendente" ? "Id_PlayerDescendente" : "Id_PlayerAscendente";

            switch (filtro)
            {
                case "Crop_NumberDescendente":
                    crops = crops.OrderByDescending(crop => crop.Crop_Number);
                    break;
                case "Id_PlayerDescendente":
                    crops = crops.OrderByDescending(crop => crop.Id_Player);
                    break;
                case "Id_PlayerAscendente":
                    crops = crops.OrderBy(crop => crop.Id_Player);
                    break;
                default:
                    crops = crops.OrderBy(crop => crop.Crop_Number);
                    break;
            }

            return View(await crops.ToListAsync());
        }

        // GET: Crops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crops = await _context.Crop
                .FirstOrDefaultAsync(m => m.Id_Crop == id);
            if (crops == null)
            {
                return NotFound();
            }

            return View(crops);
        }

        // GET: Crops/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Crop,Crop_Number,Id_Player,Deleted")] Crops crops)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crops);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crops);
        }

        // GET: Crops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crops = await _context.Crop.FindAsync(id);
            if (crops == null)
            {
                return NotFound();
            }
            return View(crops);
        }

        // POST: Crops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Crop,Crop_Number,Id_Player,Deleted")] Crops crops)
        {
            if (id != crops.Id_Crop)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crops);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CropsExists(crops.Id_Crop))
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
            return View(crops);
        }

        // GET: Crops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crops = await _context.Crop
                .FirstOrDefaultAsync(m => m.Id_Crop == id);
            if (crops == null)
            {
                return NotFound();
            }

            return View(crops);
        }

        // POST: Crops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var crop = await _context.Crop.FindAsync(id);
			if (crop!= null)
			{
				crop.Deleted = true; // Marca el registro como eliminado
				_context.Crop.Update(crop); // Actualiza el registro en el contexto
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool CropsExists(int id)
        {
            return _context.Crop.Any(e => e.Id_Crop == id);
        }
    }
}
