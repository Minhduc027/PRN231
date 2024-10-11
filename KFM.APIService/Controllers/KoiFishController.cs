using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KFM.Data.Models;
using KFM.Common;
using KFM.Service.Base;
using KFM.Service;

namespace KFM.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KoiFishController : ControllerBase
    {
        private readonly IKoiFishService _koiFishService;

        public KoiFishController(IKoiFishService koiFishService)
        {
            _koiFishService = koiFishService;
        }

        // GET: api/KoiFish
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IBusinessResult result = await _koiFishService.GetAll();
            if (result.Status == Const.SUCCESS_READ_CODE)
                return Ok(result.Data);
            return NotFound(result.Message);
        }

        // GET: api/KoiFish/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKoiFishById(int id)
        {
            IBusinessResult result = await _koiFishService.GetById(id);
            if (result.Status == Const.SUCCESS_READ_CODE)
                return Ok(result.Data);
            return NotFound(result.Message);
        }

        // POST: api/KoiFish
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] KoiFish koiFish)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            IBusinessResult result = await _koiFishService.Create(koiFish);
            if (result.Status == Const.SUCCESS_CREATE_CODE)
                return CreatedAtAction(nameof(GetKoiFishById), new { id = koiFish.KoiId }, koiFish);

            return BadRequest(result.Message);
        }

        // PUT: api/KoiFish/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] KoiFish koiFish)
        {
            if (id != koiFish.KoiId)
                return BadRequest("KoiFish ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            IBusinessResult result = await _koiFishService.Update(koiFish);
            if (result.Status == Const.SUCCESS_UPDATE_CODE)
                return NoContent();

            return BadRequest(result.Message);
        }

        // DELETE: api/KoiFish/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            IBusinessResult result = await _koiFishService.Delete(id);
            if (result.Status == Const.SUCCESS_DELETE_CODE)
                return NoContent();

            return NotFound(result.Message);
        }
    }
}