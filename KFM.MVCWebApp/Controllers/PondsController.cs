using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KFM.Data.Models;
using KFM.Service;
using KFM.Common;
using Newtonsoft.Json;
using KFM.Service.Base;

namespace KFM.MVCWebApp.Controllers
{
    public class PondsController : Controller
    {
        private readonly FA24_SE1720_PRN231_G4_KFMContext _context;
        private readonly IPondService _pondService;

        public PondsController(FA24_SE1720_PRN231_G4_KFMContext context, IPondService pondService)
        {
            _context = context;
            _pondService = pondService;
        }

        // GET: Ponds
        public async Task<IActionResult> Index()
        {
            using(var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API_ENDPOINT + "Ponds"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);//parse from api json to BusinessResult
                        if(result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Pond>>(result!.Data!.ToString()!);//parse from BusinessResult to List<Pond>
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<Pond>());
        }

        // GET: Ponds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pond = await _context.Ponds
                .FirstOrDefaultAsync(m => m.PondId == id);
            if (pond == null)
            {
                return NotFound();
            }

            return View(pond);
        }

        // GET: Ponds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ponds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PondId,Name,Image,Size,Depth,Volume,DrainCount,PumpCapacity,Description,CreatedAt,UpdatedAt")] Pond pond)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pond);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pond);
        }

        // GET: Ponds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pond = await _context.Ponds.FindAsync(id);
            if (pond == null)
            {
                return NotFound();
            }
            return View(pond);
        }

        // POST: Ponds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PondId,Name,Image,Size,Depth,Volume,DrainCount,PumpCapacity,Description,CreatedAt,UpdatedAt")] Pond pond)
        {
            if (id != pond.PondId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pond);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PondExists(pond.PondId))
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
            return View(pond);
        }

        // GET: Ponds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pond = await _context.Ponds
                .FirstOrDefaultAsync(m => m.PondId == id);
            if (pond == null)
            {
                return NotFound();
            }

            return View(pond);
        }

        // POST: Ponds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pond = await _context.Ponds.FindAsync(id);
            if (pond != null)
            {
                _context.Ponds.Remove(pond);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PondExists(int id)
        {
            return _context.Ponds.Any(e => e.PondId == id);
        }
    }
}
