using KFM.Common;
using KFM.Data.Models;
using KFM.Data;
using KFM.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFM.Service
{
    public interface IKoiFishService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> Create(KoiFish order);
        Task<IBusinessResult> GetById(int code);
        Task<IBusinessResult> Save(KoiFish order);
        Task<IBusinessResult> Update(KoiFish order);
        Task<IBusinessResult> Delete(int code);
    }

    public class KoiFishService : IKoiFishService
    {
        private UnitOfWork _unitOfWork;

        public KoiFishService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> Delete(int code)
        {
            try
            {
                var order = await _unitOfWork.KoiFishRepository.GetByIdAsync(code);
                if (order != null)
                {
                    var result = await _unitOfWork.KoiFishRepository.RemoveAsync(order);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<int> SaveAll()
        {
            return await _unitOfWork.KoiFishRepository.SaveAll();
        }
        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                var orders = await _unitOfWork.KoiFishRepository.GetAllAsync();

                if (orders == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orders);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(int code)
        {
            try
            {
                #region Business rule
                #endregion

                var order = await _unitOfWork.KoiFishRepository.GetByIdAsync(code);

                if (order == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, order);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }

        }

        public async Task<IBusinessResult> Save(KoiFish koiFish)
        {
            try
            {
                int result = -1;
                var pond = await _unitOfWork.KoiFishRepository.GetByIdAsync(koiFish.KoiId);
                if (pond == null)
                {
                    result = await _unitOfWork.KoiFishRepository.CreateAsync(koiFish);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, koiFish);
                    }
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
                else
                {
                    result = await _unitOfWork.KoiFishRepository.UpdateAsync(koiFish);
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

        public async Task<IBusinessResult> Update(KoiFish koiFish)
        {
            try
            {

                int result = await _unitOfWork.KoiFishRepository.UpdateAsync(koiFish);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Create(KoiFish koiFish)
        {
            try
            {

                int result = await _unitOfWork.KoiFishRepository.CreateAsync(koiFish);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
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


