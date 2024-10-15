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
using KFM.Service.Base;
using Newtonsoft.Json;

namespace KFM.MVCWebApp.Controllers
{
    public class SaltRequirementsController : Controller
    {
        private readonly FA24_SE1720_PRN231_G4_KFMContext _context;
        private readonly ISaltRequirementService _saltRequirementService;
        private readonly IPondService _pondService;

        public SaltRequirementsController(FA24_SE1720_PRN231_G4_KFMContext context,
            ISaltRequirementService service,
            IPondService pondService)
        {
            _context = context;
            _saltRequirementService = service;
            _pondService = pondService;
        }

        // GET: SaltRequirements
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API_ENDPOINT + "SaltRequirements"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);//parse from api json to BusinessResult
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<SaltRequirementDto>>(result!.Data!.ToString()!);//parse from BusinessResult to List<Pond>
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<SaltRequirementDto>());
           
        }

        // GET: SaltRequirements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API_ENDPOINT + "SaltRequirements/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);//parse from api json to BusinessResult
                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<SaltRequirementDto>(result!.Data!.ToString()!);//parse from BusinessResult to List<Pond>
                            return View(data);
                        }
                    }
                }
            }
            return View(new SaltRequirementDto());
        }

        // GET: SaltRequirements/Create
        private async Task<List<Pond>> GetList()
        {
            var result = await _pondService.GetAll();
            return (List<Pond>)result.Data!;
        }
        public async Task<IActionResult> Create()
        {
            //ViewData["PondId"] = new SelectList(_context.Ponds, "PondId", "PondId");
            ViewData["PondId"] = new SelectList(await this.GetList(), "PondId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaltId,PondId,RequiredSaltAmount,Notes,CreatedAt")] SaltRequirement saltRequirement)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.API_ENDPOINT + "SaltRequirements/", saltRequirement))
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
                ViewData["PondId"] = new SelectList(await this.GetList(), "PondId", "Name");
                return View(saltRequirement);
            }
            /*if (ModelState.IsValid)
            {
                _context.Add(saltRequirement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PondId"] = new SelectList(_context.Ponds, "PondId", "PondId", saltRequirement.PondId);
            return View(saltRequirement);*/
        }


        public async Task<IActionResult> Edit(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

            var saltRequirement = await _context.SaltRequirements.FindAsync(id);
            if (saltRequirement == null)
            {
                return NotFound();
            }*/
            var saltRequirement = new SaltRequirement();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API_ENDPOINT + "SaltRequirements/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(context);

                        if (result != null && result.Data != null)
                        {
                            // Kiểm tra xem dữ liệu có phải là một đối tượng hay không
                            saltRequirement = JsonConvert.DeserializeObject<SaltRequirement>(result.Data.ToString());
                        }
                    }
                }
            }
            //ViewData["PondId"] = new SelectList(_context.Ponds, "PondId", "PondId", saltRequirement.PondId);
            ViewData["PondId"] = new SelectList(await this.GetList(), "PondId", "Name", saltRequirement!.PondId);
            return View(saltRequirement);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaltId,PondId,RequiredSaltAmount,Notes,CreatedAt")] SaltRequirement saltRequirement)
        {
            bool updateStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.API_ENDPOINT + "SaltRequirements/", saltRequirement))
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
            ViewData["PondId"] = new SelectList(_context.Ponds, "PondId", "Name", saltRequirement.PondId);
            return View(saltRequirement);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

            var saltRequirement = await _context.SaltRequirements
                .Include(s => s.Pond)
                .FirstOrDefaultAsync(m => m.SaltId == id);
            if (saltRequirement == null)
            {
                return NotFound();
            }

            return View(saltRequirement);*/
            if (id == null)
            {
                return NotFound();
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API_ENDPOINT + "SaltRequirements/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(context);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<SaltRequirementDto>(result.Data.ToString());
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
                    using (var response = await httpClient.DeleteAsync(Const.API_ENDPOINT + "SaltRequirements/" + id))
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
