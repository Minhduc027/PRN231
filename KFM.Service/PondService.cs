using KFM.Common;
using KFM.Data;
using KFM.Data.Models;
using KFM.Service.Base;

namespace KFM.Service;

public interface IPondService
{
    Task<IBusinessResult> GetAll();
    Task<IBusinessResult> GetById(int id);
    Task<IBusinessResult> DeleteById(int id);
    //Task<IBusinessResult> Update(Pond p);
    Task<IBusinessResult> Save(Pond p);
}
public class PondService : IPondService
{
    private readonly UnitOfWork _unitOfWork;
    public PondService()
    {
        _unitOfWork ??= new UnitOfWork();
    }

    public async Task<IBusinessResult> Save(Pond p)
    {
        try
        {
            int result = -1;
            if(p != null && p.PondId == -1)
            {
                result = await _unitOfWork.PondRepository.CreateAsync(p);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, p);
                }
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
            else
            {
                result = await _unitOfWork.PondRepository.UpdateAsync(p);
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

    public async Task<IBusinessResult> DeleteById(int id)
    {
        try
        {
            var pond = await _unitOfWork.PondRepository.GetByIdAsync(id);
            var result = await _unitOfWork.PondRepository.RemoveAsync(pond);
            if (result)
            {
                return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, pond);
            }
            return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
        }catch (Exception ex)
        {
            return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        }
    }

    public async Task<IBusinessResult> GetAll()
    {
        try
        {
            var result = await _unitOfWork.PondRepository.GetAllAsync();
            if (result == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
        }catch(Exception ex)
        {
            return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        }
    }

    public async Task<IBusinessResult> GetById(int id)
    {
        try
        {
            var result = await _unitOfWork.PondRepository.GetByIdAsync(id);
            if (result == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
        }catch (Exception ex)
        {
            return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        }
    }

    /*public async Task<IBusinessResult> Update(Pond p)
    {
        try
        {
            var result = await _unitOfWork.PondRepository.UpdateAsync(p);
            if (result > 0)
            {
                return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, result);
            }
            return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
        }catch(Exception ex)
        {
            return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        }
    }*/
}
