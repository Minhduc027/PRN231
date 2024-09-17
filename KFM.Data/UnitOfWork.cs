using KFM.Data.Models;
using KFM.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFM.Data;

public class UnitOfWork
{
    private PondRepository pondRepository;
    private FA24_SE1720_PRN231_G4_KFMContext context;
    public UnitOfWork()
    {
        context??= new FA24_SE1720_PRN231_G4_KFMContext();
    }
    public PondRepository PondRepository
    {
        get { return pondRepository ??= new PondRepository(); }
    }
}
