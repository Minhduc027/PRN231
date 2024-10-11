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
    private FoodRepository foodRepository;
    private WaterRepository waterRepository;
    private KoiFishRepository _koiFish;
    private KoiGrowthRepository _koiGrowth;


    private SaltRequirementRepository saltRequirementRepository;
    private FA24_SE1720_PRN231_G4_KFMContext context;
    public UnitOfWork()
    {
        context??= new FA24_SE1720_PRN231_G4_KFMContext();
    }
    public PondRepository PondRepository
    {
        get { return pondRepository ??= new PondRepository(); }
    }
    public SaltRequirementRepository SaltRequirementRepository
    {
        get { return saltRequirementRepository ??= new SaltRequirementRepository(); }
    }

    public FoodRepository FoodRepository
    {
        get { return foodRepository ??= new FoodRepository();}
    }

    public WaterRepository WaterRepository
    {
        get { return waterRepository  ??= new WaterRepository(); }
    }

    public KoiFishRepository KoiFishRepository
    {
        get
        {
            return _koiFish ??= new KoiFishRepository(context);
        }
    }

}
