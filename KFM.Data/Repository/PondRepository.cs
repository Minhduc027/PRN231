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
}
