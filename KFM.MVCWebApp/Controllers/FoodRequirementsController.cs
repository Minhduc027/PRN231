using KFM.Common;
using KFM.Common.Food;
using KFM.Common.Water;
using KFM.Data.Models;
using KFM.Service;
using KFM.Service.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KFM.MVCWebApp.Controllers
{
    public class FoodRequirementsController : Controller
    {
        private readonly FA24_SE1720_PRN231_G4_KFMContext _context;
        private readonly IFoodService _foodService;

        public FoodRequirementsController(FA24_SE1720_PRN231_G4_KFMContext context, IFoodService foodService)
        {
            _context = context;
            _foodService = foodService;
        }

        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API_ENDPOINT + "FoodRequirements"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);//parse from api json to BusinessResult
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<FoodRequirement>>(result!.Data!.ToString()!);//parse from BusinessResult to List<Pond>
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<FoodRequirement>());
        }

        //public async Task<IActionResult> Create()
        //{

        //    ViewData["KoiId"] = new SelectList(await this.GetList(), "KoiId", "Name");
        //    return View();
        //}

        //private async Task<List<KoiFish>> GetList()
        //{
        //    var result = await _pondService.GetAll();
        //    return (List<KoiFish>)result.Data!;
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodRequirement food)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.API_ENDPOINT + "FoodRequirements/", food))
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

                return View(food);
            }

        }

        public async Task<IActionResult> Details(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API_ENDPOINT + "FoodRequirements/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);//parse from api json to BusinessResult
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<FoodRequirementsDto>(result!.Data!.ToString()!);//parse from BusinessResult to List<Pond>
                            return View(data);
                        }
                    }
                }
            }
            return View(new FoodRequirementsDto());
        }


        public async Task<IActionResult> Edit(int? id)
        {

            var foodRequirement = new FoodRequirement();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API_ENDPOINT + "FoodRequirements/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(context);

                        if (result != null && result.Data != null)
                        {
                            // Kiểm tra xem dữ liệu có phải là một đối tượng hay không
                            foodRequirement = JsonConvert.DeserializeObject<FoodRequirement>(result.Data.ToString());
                        }
                    }
                }
            }

            //ViewData["PondId"] = new SelectList(await this.GetList(), "PondId", "Name", waterParameter!.PondId);
            return View(foodRequirement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FoodRequirement foodRequirement)
        {
            bool updateStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.API_ENDPOINT + "FoodRequirements/", foodRequirement))
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
            ViewData["KoiId"] = new SelectList(_context.KoiFishes, "KoiId", "KoiId", foodRequirement.KoiId);
            return View(foodRequirement);
        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API_ENDPOINT + "FoodRequirements/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(context);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<FoodRequirementsDto>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new KoiFish());
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
                    using (var response = await httpClient.DeleteAsync(Const.API_ENDPOINT + "FoodRequirements/" + id))
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
