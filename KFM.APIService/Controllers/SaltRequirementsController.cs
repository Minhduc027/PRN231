using Microsoft.AspNetCore.Mvc;
using KFM.Data.Models;
using KFM.Service;
using KFM.Service.Base;
using System.Drawing.Printing;

namespace KFM.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaltRequirementsController : ControllerBase
    {
        private readonly ISaltRequirementService _service;

        public SaltRequirementsController(ISaltRequirementService service)
        {
            _service = service;
        }

        // GET: api/SaltRequirements
        [HttpGet]
        public async Task<IBusinessResult> GetSaltRequirements([FromQuery]int pageNo = 1, [FromQuery] int pageSize = 10)
        {
            return await _service.GetAll(pageNo, pageSize);
        }

        // GET: api/SaltRequirements/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetSaltRequirement(int id)
        {
            var salt = await _service.GetById(id);
            return salt;
        }

        // PUT: api/SaltRequirements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IBusinessResult> PutSaltRequirement(SaltRequirement saltRequirement)
        {
            return await _service.Save(saltRequirement);
        }

        // POST: api/SaltRequirements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostSaltRequirement(SaltRequirement saltRequirement)
        {
            return await _service.Save(saltRequirement);
        }

        // DELETE: api/SaltRequirements/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteSaltRequirement(int id)
        {
            return await _service.DeleteById(id);
        }
        [HttpGet("s/{saltAmoumt}")]
        public async Task<IBusinessResult> searchSalt(double? saltAmoumt, int pageNo = 1, int pageSize = 10)
        {
            return await _service.searchSalt(saltAmoumt, pageNo, pageSize);
        }
        /*private bool SaltRequirementExists(int id)
        {
            return _context.SaltRequirements.Any(e => e.SaltId == id);
        }*/
    }
}
