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
    public class KoiFishRepository : GenericRepository_Tuan<KoiFish>
    {
        public KoiFishRepository()
        {
        }

        public KoiFishRepository(FA24_SE1720_PRN231_G4_KFMContext context) => _context = context;
    }
}
