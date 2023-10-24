using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MioMap.Models;

namespace MioMap.Controllers
{
    public class GisLayersController : Controller
    {
        private readonly MioMapDbContext _context;

        public GisLayersController(MioMapDbContext context)
        {
            _context = context;
        }

        // GET: GisLayers
        public async Task<IActionResult> Index()
        {
            return _context.GisLayers != null ?
                        View(await _context.GisLayers.ToListAsync()) :
                        Problem("Entity set 'MioMapDbContext.GisLayer'  is null.");
        }

        public async Task<IActionResult> GeLayersAsJson()
        {
            return _context.GisLayers != null ?
                        new JsonResult(await _context.GisLayers.Include(x => x.WaterPiplines).ToListAsync()) :
                        Problem("Entity set 'MioMapDbContext.GisLayer'  is null.");
        }

        // GET: GisLayers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GisLayers == null)
            {
                return NotFound();
            }

            var gisLayer = await _context.GisLayers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gisLayer == null)
            {
                return NotFound();
            }

            return View(gisLayer);
        }

        // GET: GisLayers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GisLayers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] GisLayer gisLayer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gisLayer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gisLayer);
        }

        // GET: GisLayers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GisLayers == null)
            {
                return NotFound();
            }

            var gisLayer = await _context.GisLayers.FindAsync(id);
            if (gisLayer == null)
            {
                return NotFound();
            }
            return View(gisLayer);
        }

        // POST: GisLayers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] GisLayer gisLayer)
        {
            if (id != gisLayer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gisLayer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GisLayerExists(gisLayer.Id))
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
            return View(gisLayer);
        }

        // GET: GisLayers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GisLayers == null)
            {
                return NotFound();
            }

            var gisLayer = await _context.GisLayers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gisLayer == null)
            {
                return NotFound();
            }

            return View(gisLayer);
        }

        // POST: GisLayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GisLayers == null)
            {
                return Problem("Entity set 'MioMapDbContext.GisLayer'  is null.");
            }
            var gisLayer = await _context.GisLayers.FindAsync(id);
            if (gisLayer != null)
            {
                _context.GisLayers.Remove(gisLayer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GisLayerExists(int id)
        {
            return (_context.GisLayers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private List<List<string>> GetWaterClockMatrix(List<WaterClockMatrix> data)
        {
            if (data.Count == 0)
            {
                return new List<List<string>>();
            }
            string[,] matrix = new string[data.Count, data.Count];
            var result = new List<List<string>>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var lineResult = new List<string>();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    var checkPoint = "";
                    if (i == j)
                    {
                        checkPoint = "...";
                    }
                    else
                    {
                        checkPoint = "0";
                        List<int> outIds = data[i].OutWaterClock.Split(",").Select(int.Parse).ToList();
                        foreach (var id in outIds)
                        {
                            var outClock = data.FirstOrDefault(x => ((dynamic)x).Id == id);
                            if (outClock != null && outClock.Index == j)
                            {
                                checkPoint = "1";
                            }
                        }
                    }
                    lineResult.Add(checkPoint);
                }
                result.Add(lineResult);
            }
            return result;
        }

        public IActionResult MatrixOfClockConnection()
        {
            var index = 0;
            var listWaterClock = _context.WaterClocks.ToList().Select(x => new WaterClockMatrix(x, index++)).ToList();
            var result = GetWaterClockMatrix(listWaterClock);
            return new JsonResult(result);
        }

        private static string[,] ConvertToTwoDimensionalArray(List<List<string>> listOfLists)
        {
            int rowCount = listOfLists.Count;
            int colCount = listOfLists.Max(list => list.Count);

            string[,] result = new string[rowCount, colCount];

            for (int row = 0; row < rowCount; row++)
            {
                List<string> list = listOfLists[row];
                for (int col = 0; col < list.Count; col++)
                {
                    result[row, col] = list[col];
                }
            }

            return result;
        }

        private void GetPathFromPointInMatrix(List<string> result, List<WaterClockMatrix> listWaterClockMatrix, int modelId, string[,] matrixString)
        {
            var modelInMatrix = listWaterClockMatrix.FirstOrDefault(x => x.Id == modelId);
            if (modelInMatrix == null)
            {
                return;
            }
            for (int j = 0; j < matrixString.GetLength(1); j++)
            {
                var temp = listWaterClockMatrix.FirstOrDefault(x => x.Index == j);
                if (temp != null)
                {
                    if (matrixString[modelInMatrix.Index, j] == "0")
                    {
                        result.Add(temp.Id.ToString());
                    }
                    else if (matrixString[modelInMatrix.Index, j] == "1")
                    {
                        foreach (var id in temp.OutWaterClock.Split(',').Select(int.Parse))
                        {
                            GetPathFromPointInMatrix(result, listWaterClockMatrix, id, matrixString);
                        }
                    }
                }
            }
        }

        public IActionResult PathFromPontInMatrix(int Id)
        {
            var affected = new List<string>();
            var safe = new List<string>();
            var index = 0;
            var listWaterClockMatrix = _context.WaterClocks.ToList().Select(x => new WaterClockMatrix(x, index++)).ToList();
            var matrixList = GetWaterClockMatrix(listWaterClockMatrix);
            string[,] matrixString = ConvertToTwoDimensionalArray(matrixList);
            GetPathFromPointInMatrix(affected, listWaterClockMatrix, Id, matrixString);
            safe.AddRange(listWaterClockMatrix.Where(x => !affected.Select(int.Parse).Contains(x.Id)).Select(x => x.Id.ToString()));
            return new JsonResult(
                new
                {
                    affected,
                    safe
                }
            );
        }
    }
}
