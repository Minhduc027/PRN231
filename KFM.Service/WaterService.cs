﻿using AutoMapper;
using KFM.Common;
using KFM.Common.Water;
using KFM.Data;
using KFM.Data.Models;
using KFM.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFM.Service
{
    public interface IWaterService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> DeleteById(int id);

        Task<IBusinessResult> Save(WaterParameter w);

       
    }

    public class WaterService : IWaterService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WaterService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork ??= unitOfWork;
            _mapper = mapper;
        }

        public async Task<IBusinessResult> DeleteById(int id)
        {

            try
            {
                var water = await _unitOfWork.WaterRepository.GetByIdAsync(id);

                if (water == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new WaterParameter());
                }
                else
                {
                    var result = await _unitOfWork.WaterRepository.RemoveAsync(water);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, water);
                    }
                    return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var waters = await _unitOfWork.WaterRepository.GetAllWaterReq();
                List<WaterParameterDto> result = _mapper.Map<List<WaterParameterDto>>(waters);
                if (result == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var water = await _unitOfWork.WaterRepository.GetByIdAsNotracking(id);
                var result = _mapper.Map<WaterParameterDto>(water);
                if (result == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Save(WaterParameter w)
        {
            try
            {
                int result = -1;
                var water = await _unitOfWork.WaterRepository.GetByIdAsNotracking(w.ParameterId);
                if (water == null)
                {
                    result = await _unitOfWork.WaterRepository.CreateAsync(w);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, w);
                    }
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
                else
                {
                    var newDto = _mapper.Map<WaterParameterUpdateDto>(w);
                    var newEntity = _mapper.Map<WaterParameter>(newDto);
                    newEntity.CreatedAt = water.CreatedAt;
                    result = await _unitOfWork.WaterRepository.UpdateAsync(newEntity);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, result);
                    }
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }
    }
}
