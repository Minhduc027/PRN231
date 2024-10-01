using KFM.Data.Models;
using KFM.Service;
using KFM.Service.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KFM.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodRequirementController : ControllerBase
    {

        private readonly IFoodService _foodService;

        public FoodRequirementController(IFoodService foodService)
        {
            //_context = context;
            _foodService = foodService;
        }


        [HttpGet]
        public async Task<IBusinessResult> GetAllFoodRequirements()
        {

            return await _foodService.GetAll();
        }

        
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetFoodRequirement(int id)
        {
            var pond = await _foodService.GetById(id);
            return pond;
        }

        [HttpPut]
        public async Task<IBusinessResult> UpdateFoodRequirement(FoodRequirement foodRequirement)
        {
            return await _foodService.Save(foodRequirement);
        }


        [HttpPost]
        public async Task<IBusinessResult> AddFoodRequirement(FoodRequirement foodRequirement)
        {
            return await _foodService.Save(foodRequirement);
        }


        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeletePond(int id)
        {
            return await _foodService.DeleteById(id);
        }
     }
}
