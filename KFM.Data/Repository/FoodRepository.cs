using KFM.Data.Base;
using KFM.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFM.Data.Repository
{
    public class FoodRepository : GenericRepository<FoodRequirement>
    {
        public FoodRepository() { }
        public FoodRepository(FA24_SE1720_PRN231_G4_KFMContext context) => _context = context;

        public async Task<FoodRequirement?> GetByIdAsNotracking(int id)
        {
            var food = await _context.FoodRequirements.Include(s => s.Koi).AsNoTracking().FirstOrDefaultAsync(f => f.FoodId == id);
            return food != null ? food : null;
        }

        public async Task<List<FoodRequirement>?> GetAllFoodReq()
        {
            var foodRequirement = await _context.FoodRequirements.Include(s => s.Koi).AsNoTracking().ToListAsync();
            return foodRequirement != null ? foodRequirement : null;
        }
    }
}
