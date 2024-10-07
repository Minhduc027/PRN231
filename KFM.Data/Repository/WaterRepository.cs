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
    public class WaterRepository : GenericRepository<WaterParameter>
    {
        public WaterRepository() { }
        public WaterRepository(FA24_SE1720_PRN231_G4_KFMContext context) => _context = context;

        public async Task<WaterParameter> GetByIdAsNotracking(int id)
        {
            var water = await _context.WaterParameters.AsNoTracking().FirstOrDefaultAsync(w => w.ParameterId == id);
            return water;
        }
    }
}
