using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MioMap.Models;

namespace MioMap.Controllers
{
    public class WaterPiplinesController : Controller
    {
        private readonly MioMapDbContext _context;

        public WaterPiplinesController(MioMapDbContext context)
        {
            _context = context;
        }

        // GET: WaterPiplines
        public async Task<IActionResult> Index()
        {
            return _context.WaterPiplines != null ?
                        View(await _context.WaterPiplines.ToListAsync()) :
                        Problem("Entity set 'MioMapDbContext.WaterPipline'  is null.");
        }

        public IActionResult GetWaterPiplinesAsJson(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                keyword = "";
            }
            return _context.WaterPiplines != null ?
                        new JsonResult(_context.WaterPiplines.ToList().Where(x => x.Tags.ToLower().Contains(keyword.ToLower()))) :
                        Problem("Entity set 'MioMapDbContext.WaterPipline'  is null.");
        }

        // GET: WaterPiplines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WaterPiplines == null)
            {
                return NotFound();
            }

            var waterPipline = await _context.WaterPiplines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (waterPipline == null)
            {
                return NotFound();
            }

            return View(waterPipline);
        }

        // GET: WaterPiplines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WaterPiplines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Lat1,Long1,Lat2,Long2,Title,Description,InfoBoxTitle,InfoBoxDescription,Tags")] WaterPipline waterPipline)
        {
            if (ModelState.IsValid)
            {
                _context.Add(waterPipline);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(waterPipline);
        }

        // GET: WaterPiplines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WaterPiplines == null)
            {
                return NotFound();
            }

            var waterPipline = await _context.WaterPiplines.FindAsync(id);
            if (waterPipline == null)
            {
                return NotFound();
            }
            return View(waterPipline);
        }

        // POST: WaterPiplines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Lat1,Long1,Lat2,Long2,Title,Description,InfoBoxTitle,InfoBoxDescription,Tags")] WaterPipline waterPipline)
        {
            if (id != waterPipline.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(waterPipline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WaterPiplineExists(waterPipline.Id))
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
            return View(waterPipline);
        }

        // GET: WaterPiplines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WaterPiplines == null)
            {
                return NotFound();
            }

            var waterPipline = await _context.WaterPiplines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (waterPipline == null)
            {
                return NotFound();
            }

            return View(waterPipline);
        }

        // POST: WaterPiplines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WaterPiplines == null)
            {
                return Problem("Entity set 'MioMapDbContext.WaterPipline'  is null.");
            }
            var waterPipline = await _context.WaterPiplines.FindAsync(id);
            if (waterPipline != null)
            {
                _context.WaterPiplines.Remove(waterPipline);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WaterPiplineExists(int id)
        {
            return (_context.WaterPiplines?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
