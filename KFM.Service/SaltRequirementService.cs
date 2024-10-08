using AutoMapper;
using KFM.Common;
using KFM.Data;
using KFM.Data.Models;
using KFM.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFM.Service;
public interface ISaltRequirementService
{
    Task<IBusinessResult> GetAll();
    Task<IBusinessResult> GetById(int id);
    Task<IBusinessResult> DeleteById(int id);
    Task<IBusinessResult> Save(SaltRequirement entity);
}
public class SaltRequirementService : ISaltRequirementService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SaltRequirementService(IMapper mapper)
    {
        _unitOfWork ??= new UnitOfWork();
        _mapper = mapper;
    }

    public async Task<IBusinessResult> DeleteById(int id)
    {
        try
        {
            var salt = await _unitOfWork.SaltRequirementRepository.GetByIdAsync(id);
            if (salt == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Pond());
            }
            else
            {
                var result = await _unitOfWork.SaltRequirementRepository.RemoveAsync(salt);
                if (result)
                {
                    return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, salt);
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
            var salts = await _unitOfWork.SaltRequirementRepository.GetAllSalt();
            List<SaltRequirementDto> result = _mapper.Map<List<SaltRequirementDto>>(salts);
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
            var salts = await _unitOfWork.SaltRequirementRepository.GetByIdAsNotracking(id);
            SaltRequirementDto result = _mapper.Map<SaltRequirementDto>(salts);
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

    public async Task<IBusinessResult> Save(SaltRequirement entity)
    {
        try
        {
            int result = -1;
            var pond = await _unitOfWork.SaltRequirementRepository.GetByIdAsNotracking(entity.SaltId);
            if (pond == null)
            {
                result = await _unitOfWork.SaltRequirementRepository.CreateAsync(entity);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, entity);
                }
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
            else
            {
                result = await _unitOfWork.SaltRequirementRepository.UpdateAsync(entity);
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
