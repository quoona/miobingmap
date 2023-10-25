using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MioMap;
using MioMap.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace MioMap.Controllers
{
    public class WaterClocksController : Controller
    {
        private readonly MioMapDbContext _context;

        public WaterClocksController(MioMapDbContext context)
        {
            _context = context;
        }

        // GET: WaterClocks
        public async Task<IActionResult> Index()
        {
            return _context.WaterClocks != null ?
                        View(await _context.WaterClocks.ToListAsync()) :
                        Problem("Entity set 'MioMapDbContext.WaterClocks'  is null.");
        }

        public IActionResult WaterClockMap()
        {
            return View();
        }

        public async Task<IActionResult> GetWaterClockAsJson()
        {

            return _context.WaterClocks != null ?
                        new JsonResult(await _context.WaterClocks.ToListAsync()) :
                        Problem("Entity set 'MioMapDbContext.WaterClocks'  is null.");
        }

        public IActionResult SearchWaterClock(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                keyword = "";
            }

            return _context.WaterPiplines != null ?
                        new JsonResult(_context.WaterClocks.ToList().Where(x => x.Title.ToLower().Contains(keyword.ToLower()))) :
                        Problem("Entity set 'MioMapDbContext.WaterPipline'  is null.");
        }


        [HttpPost]
        public async Task<IActionResult> GenerateWaterClock([FromBody] GenerateWCModel model)
        {

            foreach (var waterClock in model.WaterClocks)
            {
                if (string.IsNullOrEmpty(waterClock.InWaterClock))
                {
                    waterClock.InWaterClock = "";
                }
                if (string.IsNullOrEmpty(waterClock.OutWaterClock))
                {
                    waterClock.OutWaterClock = "";
                }
                waterClock.InfoBoxTitle = waterClock.Title;
                waterClock.InfoBoxDescription = waterClock.Description;
                var isExist = _context.WaterClocks.ToList().FirstOrDefault(x => x.Title.ToLower() == waterClock.Title.ToLower());

                if (isExist is null) _context.Add(waterClock);

            }
            await _context.SaveChangesAsync();
            Response.StatusCode = StatusCodes.Status200OK;
            return new JsonResult("");
        }

        // GET: WaterClocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WaterClocks == null)
            {
                return NotFound();
            }

            var waterClock = await _context.WaterClocks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (waterClock == null)
            {
                return NotFound();
            }

            return View(waterClock);
        }

        // GET: WaterClocks/Create
        public IActionResult Create()
        {
            ViewData["ListWaterClock"] = _context.WaterClocks.Where(x => x.Id != 0).ToList();
            var model = new CRUDWaterClockModel();
            model.WaterClockStatus = 0;
            return View(model);
        }

        // POST: WaterClocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CRUDWaterClockModel model)
        {

            var inWaterClockIds = "";
            var outWaterClockIds = "";
            if (model.InWaterClock.Any())
            {
                inWaterClockIds = string.Join(",", model.InWaterClock);
            }
            if (model.OutWaterClock.Any())
            {
                outWaterClockIds = string.Join(",", model.OutWaterClock);
            }

            WaterClock waterClock = new()
            {
                Title = model.Title,
                Description = model.Description,
                Address = model.Address,
                Lat = model.Lat,
                Long = model.Long,
                InfoBoxDescription = model.Description,
                InfoBoxTitle = model.Title,
                InWaterClock = inWaterClockIds,
                OutWaterClock = outWaterClockIds
            };

            ViewData["ListWaterClock"] = _context.WaterClocks.Where(x => x.Id != 0).ToList();

            _context.Add(waterClock);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //return View(waterClock);
        }

        // GET: WaterClocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["ListWaterClock"] = _context.WaterClocks.Where(x => x.Id != 0).ToList();

            if (id == null || _context.WaterClocks == null)
            {
                return NotFound();
            }

            var waterClock = await _context.WaterClocks.FindAsync(id);
            if (waterClock == null)
            {
                return NotFound();
            }

            List<int> inWaterClock = new();
            List<int> outWaterClock = new();
            if (!string.IsNullOrEmpty(waterClock.InWaterClock))
            {
                inWaterClock = waterClock.InWaterClock.Split(',').Select(int.Parse).ToList();
            }
            if (!string.IsNullOrEmpty(waterClock.OutWaterClock))
            {
                outWaterClock = waterClock.OutWaterClock.Split(',').Select(int.Parse).ToList();
            }

            CRUDWaterClockModel model = new()
            {
                Id = waterClock.Id,
                Title = waterClock.Title,
                Description = waterClock.Description,
                Address = waterClock.Address,
                Lat = waterClock.Lat,
                Long = waterClock.Long,
                InWaterClock = inWaterClock,
                OutWaterClock = outWaterClock,
                WaterClockStatus = waterClock.WaterClockStatus,
            };

            return View(model);
        }

        // POST: WaterClocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WaterClock waterClock)
        {
            ViewData["ListWaterClock"] = _context.WaterClocks.Where(x => x.Id != 0).AsNoTracking().ToList();
            if (id != waterClock.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(waterClock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WaterClockExists(waterClock.Id))
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
            return View(waterClock);
        }

        // GET: WaterClocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WaterClocks == null)
            {
                return NotFound();
            }

            var waterClock = await _context.WaterClocks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (waterClock == null)
            {
                return NotFound();
            }

            return View(waterClock);
        }

        // POST: WaterClocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WaterClocks == null)
            {
                return Problem("Entity set 'MioMapDbContext.WaterClocks'  is null.");
            }
            var waterClock = await _context.WaterClocks.FindAsync(id);
            if (waterClock != null)
            {
                _context.WaterClocks.Remove(waterClock);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WaterClockExists(int id)
        {
            return (_context.WaterClocks?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
