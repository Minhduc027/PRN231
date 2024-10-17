using KFM.Data.Base;
using KFM.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KFM.Data.Repository;

public class PondRepository: GenericRepository<Pond>
{
    public PondRepository() { }
    public PondRepository(FA24_SE1720_PRN231_G4_KFMContext context) => _context = context;
    public async Task<int> Count()
    {
        var result = await _context.Ponds.Where(p => p.PumpCapacity > 200).CountAsync();
        return result;
    }
    public async Task<Pond> GetByIdAsNotracking(int id)
    {
        var pond  = await _context.Ponds.AsNoTracking().FirstOrDefaultAsync(p => p.PondId == id);
        return pond;
    }
    public async Task<List<Pond>> searchPond(string name, int? drainCount, double? size, double? depth, double? volume, double? pumpCapacity)
    {
        var ponds = _context.Ponds.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            ponds = ponds.Where(p => p.Name.Contains(name)); 
        }

        if (drainCount.HasValue && drainCount > 0)
        {
            ponds = ponds.Where(p => p.DrainCount == drainCount);
        }

        if (size.HasValue)
        {
            ponds = ponds.Where(p => p.Size == size);
        }

        if (depth.HasValue)
        {
            ponds = ponds.Where(p => p.Depth == depth);
        }

        if (volume.HasValue)
        {
            ponds = ponds.Where(p => p.Volume == volume);
        }

        if (pumpCapacity.HasValue)
        {
            ponds = ponds.Where(p => p.PumpCapacity == pumpCapacity);
        }

        return await ponds.ToListAsync();
    }
}
