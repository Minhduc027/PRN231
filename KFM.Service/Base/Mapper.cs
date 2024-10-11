using AutoMapper;
using KFM.Common;
using KFM.Common.Water;
using KFM.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFM.Service.Base;

public class Mapper: Profile
{
    public Mapper() { 
        CreateMap<Pond, PondDto>();
        CreateMap<SaltRequirement, SaltRequirementDto>()
            .ForMember(dest => dest.Pond, otp => otp.MapFrom(src => src.Pond));


        CreateMap<WaterParameter, WaterParameterDto>()
            .ForMember(dest => dest.Pond, otp => otp.MapFrom(src => src.Pond));
        CreateMap<WaterParameter, WaterParameterUpdateDto>()
            .ForMember(dest => dest.Pond, otp => otp.MapFrom(src => src.Pond)).ReverseMap();
    }

}
