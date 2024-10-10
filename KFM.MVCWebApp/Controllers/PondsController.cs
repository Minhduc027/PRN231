using Microsoft.AspNetCore.Mvc;
using KFM.Data.Models;
using KFM.Service;
using KFM.Common;
using Newtonsoft.Json;
using KFM.Service.Base;

namespace KFM.MVCWebApp.Controllers;

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
    public async Task<List<Pond>> GetList()
    {
        var result = await _pondService.GetAll();
        return (List<Pond>)result.Data!;
    }

    // GET: Ponds/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(Const.API_ENDPOINT + "Ponds/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);//parse from api json to BusinessResult
                    if (result != null && result.Data != null)
                    {
                        var data = JsonConvert.DeserializeObject<Pond>(result!.Data!.ToString()!);//parse from BusinessResult to List<Pond>
                        return View(data);
                    }
                }
            }
        }
        return View(new Pond());
        /*if (id == null)
        {
            return NotFound();
        }

        var pond = await _context.Ponds
            .FirstOrDefaultAsync(m => m.PondId == id);
        if (pond == null)
        {
            return NotFound();
        }

        return View(pond);*/
    }

    // GET: Ponds/Create
    public IActionResult Create()
    {
        //ViewData["YourViewData"] = new SelectList(await this.GetList(), "DataId", "Lable", Entity.DataId);
        return View();
    }

    // POST: Ponds/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(/*[Bind("PondId,Name,Image,Size,Depth,Volume,DrainCount,PumpCapacity,Description,CreatedAt,UpdatedAt")]*/Pond pond)
    {
        bool saveStatus = false;
        if (ModelState.IsValid)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync(Const.API_ENDPOINT + "Ponds/", pond))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);//parse from api json to BusinessResult
                        if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
                        {
                            saveStatus = true;
                        }
                    }
                }
            }
        }
        if (saveStatus)
        {
            return RedirectToAction(nameof(Index));
        }
        else
        {
            //ViewData["YourViewData"] = new SelectList(await this.GetList(), "DataId", "Lable", Entity.DataId);
            return View(pond);
        }         
        /*if (ModelState.IsValid)
        {
            _context.Add(pond);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(pond);*/
    }

    // GET: Ponds/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        var pond = new Pond();
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(Const.API_ENDPOINT + "Ponds/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var context = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(context);

                    if (result != null && result.Data != null)
                    {
                        // Kiểm tra xem dữ liệu có phải là một đối tượng hay không
                        pond = JsonConvert.DeserializeObject<Pond>(result.Data.ToString());
                    }
                }
            }
        }
        //ViewData["YourViewData"] = new SelectList(await this.GetList(), "DataId", "Lable", Entity.DataId);
        return View(pond);
        /*if (id == null)
        {
            return NotFound();
        }

        var pond = await _context.Ponds.FindAsync(id);
        if (pond == null)
        {
            return NotFound();
        }
        return View(pond);*/
    }

    // POST: Ponds/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("PondId,Name,Image,Size,Depth,Volume,DrainCount,PumpCapacity,Description,CreatedAt,UpdatedAt")] Pond pond)
    {
        bool updateStatus = false;
        if (ModelState.IsValid)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsJsonAsync(Const.API_ENDPOINT + "Ponds/", pond))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
                        {
                            updateStatus = true;
                        }
                    }
                }
            }
            /*try
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
            return RedirectToAction(nameof(Index));*/
        }
        if (updateStatus)
        {
            return RedirectToAction(nameof(Index));
        }
        //ViewData["YourViewData"] = new SelectList(await this.GetList(), "DataId", "Lable", Entity.DataId);
        return View(pond);
    }

    // GET: Ponds/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(Const.API_ENDPOINT + "Ponds/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var context = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(context);

                    if (result != null && result.Data != null)
                    {
                        var data = JsonConvert.DeserializeObject<Pond>(result.Data.ToString());
                        return View(data);
                    }
                }
            }
        }
        return View(new Pond());
    }

    // POST: Ponds/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        bool deleteStatus = false;
        if (ModelState.IsValid)
        {
            using(var httpClient = new HttpClient())
            {
                using(var response = await httpClient.DeleteAsync(Const.API_ENDPOINT + "Ponds/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult> (content);
                        if( result != null && result.Status == Const.SUCCESS_DELETE_CODE)
                        {
                            deleteStatus = true;
                        }
                    }
                }
            }
        }
        if (deleteStatus)
        {
            return RedirectToAction(nameof(Index));
        }
        else
        {
            return RedirectToAction(nameof(Delete));
        }
    }

    private bool PondExists(int id)
    {
        return _context.Ponds.Any(e => e.PondId == id);
    }
}
