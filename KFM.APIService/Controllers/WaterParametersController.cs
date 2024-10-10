using KFM.Data.Models;
using KFM.Service.Base;
using KFM.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KFM.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterParametersController : ControllerBase
    {
        private readonly IWaterService _waterService;

        public WaterParametersController(IWaterService waterService)
        {
            //_context = context;
            _waterService = waterService;
        }


        [HttpGet]
        public async Task<IBusinessResult> GetAllWaterRequirements()
        {

            return await _waterService.GetAll();
        }


        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetWaterRequirement(int id)
        {
            var pond = await _waterService.GetById(id);
            return pond;
        }

        [HttpPut]
        public async Task<IBusinessResult> UpdateWaterParameter(WaterParameter waterParameter)
        {
            return await _waterService.Save(waterParameter);
        }


        [HttpPost]
        public async Task<IBusinessResult> AddWaterParameter(WaterParameter waterParameter)
        {
            return await _waterService.Save(waterParameter);
        }


        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeletePond(int id)
        {
            return await _waterService.DeleteById(id);
        }
    }
}
