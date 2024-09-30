using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KFM.Data.Models;
using KFM.Service;
using KFM.Service.Base;

namespace KFM.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PondsController : ControllerBase
    {
        //private readonly FA24_SE1720_PRN231_G4_KFMContext _context;
        private readonly IPondService _pondService;

        public PondsController(IPondService pondService)
        {
           //_context = context;
            _pondService = pondService;
        }

        // GET: api/Ponds
        [HttpGet]
        public async Task<IBusinessResult> GetPonds()
        {

            return await _pondService.GetAll();
        }

        // GET: api/Ponds/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetPond(int id)
        {
            var pond = await _pondService.GetById(id);
            return pond;
        }

        // PUT: api/Ponds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IBusinessResult> PutPond(Pond pond)
        {
            return await _pondService.Save(pond);
            /*if (id != pond.PondId)
            {
                return BadRequest();
            }

            _context.Entry(pond).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PondExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();*/
        }

        // POST: api/Ponds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostPond(Pond pond)
        {
            return await _pondService.Save(pond);
        }

        // DELETE: api/Ponds/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeletePond(int id)
        {
            return await _pondService.DeleteById(id);
            /*var pond = await _context.Ponds.FindAsync(id);
            if (pond == null)
            {
                return NotFound();
            }

            _context.Ponds.Remove(pond);
            await _context.SaveChangesAsync();

            return NoContent();*/
        }

        /*private bool PondExists(int id)
        {
            return _context.Ponds.Any(e => e.PondId == id);
        }*/
    }
}
