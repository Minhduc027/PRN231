using KFM.Common;
using KFM.Data.Models;
using KFM.Service;
using KFM.Service.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KFM.MVCWebApp.Controllers
{
    public class WaterParametersController : Controller
    {
        private readonly FA24_SE1720_PRN231_G4_KFMContext _context;
        private readonly IWaterService _waterService;
        private readonly IPondService _pondService;

        public WaterParametersController(FA24_SE1720_PRN231_G4_KFMContext context, IWaterService waterService, IPondService pondService)
        {
            _context = context;
            _waterService = waterService;
            _pondService = pondService;
        }

        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API_ENDPOINT + "WaterParameters"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);//parse from api json to BusinessResult
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<WaterParameter>>(result!.Data!.ToString()!);//parse from BusinessResult to List<Pond>
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<WaterParameter>());
        }
        public async Task<IActionResult> Create()
        {
            //ViewData["PondId"] = new SelectList(_context.Ponds, "PondId", "PondId");
            ViewData["PondId"] = new SelectList(await this.GetList(), "PondId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WaterParameter water)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.API_ENDPOINT + "WaterParameters/", water))
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
                
                return View(water);
            }
            
        }

        private async Task<List<Pond>> GetList()
        {
            var result = await _pondService.GetAll();
            return (List<Pond>)result.Data!;
        }


        public async Task<IActionResult> Details(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API_ENDPOINT + "WaterParameters/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);//parse from api json to BusinessResult
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<WaterParameterDto>(result!.Data!.ToString()!);//parse from BusinessResult to List<Pond>
                            return View(data);
                        }
                    }
                }
            }
            return View(new SaltRequirementDto());
        }


        public async Task<IActionResult> Edit(int? id)
        {
          
            var waterParameter = new WaterParameter();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API_ENDPOINT + "WaterParameters/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(context);

                        if (result != null && result.Data != null)
                        {
                            // Kiểm tra xem dữ liệu có phải là một đối tượng hay không
                            waterParameter = JsonConvert.DeserializeObject<WaterParameter>(result.Data.ToString());
                        }
                    }
                }
            }
            
            ViewData["PondId"] = new SelectList(await this.GetList(), "PondId", "Name", waterParameter!.PondId);
            return View(waterParameter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WaterParameter waterParameter)
        {
            bool updateStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.API_ENDPOINT + "WaterParameters/", waterParameter))
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
            }
            if (updateStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["PondId"] = new SelectList(_context.Ponds, "PondId", "PondId", waterParameter.PondId);
            return View(waterParameter);
        }


        public async Task<IActionResult> Delete(int? id)
        {
          
            if (id == null)
            {
                return NotFound();
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API_ENDPOINT + "WaterParameters/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(context);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<WaterParameterDto>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new Pond());
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.API_ENDPOINT + "WaterParameters/" + id))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_DELETE_CODE)
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
    }
}
