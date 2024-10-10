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

        public async Task<WaterParameter?> GetByIdAsNotracking(int id)
        {
            var waterRequirement = await _context.WaterParameters.Include(s => s.Pond).AsNoTracking().FirstOrDefaultAsync(w => w.ParameterId == id);
            return waterRequirement != null ? waterRequirement : null;
        }

        public async Task<List<WaterParameter>?> GetAllWaterReq()
        {
            var waterRequirement = await _context.WaterParameters.Include(s => s.Pond).AsNoTracking().ToListAsync();
            return waterRequirement != null ? waterRequirement : null;
        }
    }
}
