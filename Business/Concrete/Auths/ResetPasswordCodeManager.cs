﻿using Business.Abstract.Auths;
using Business.Constants;
using Core.Entities.Concrete.Auth;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs.Users;

namespace Business.Concrete.Auths
{
    public class ResetPasswordCodeManager : IResetPasswordCodeService
    {
        private IResetPasswordCodeDal _resetPasswordCodeDal;
        public ResetPasswordCodeManager(IResetPasswordCodeDal resetPasswordCodeDal)
        {
            _resetPasswordCodeDal = resetPasswordCodeDal;
        }
        public IResult Add(ResetPasswordCode resetPasswordCode)
        {
            _resetPasswordCodeDal.Add(resetPasswordCode);
            return new SuccessResult(Messages.Added);
        }
        public IResult ConfirmResetCode(string code)
        {
            var result = _resetPasswordCodeDal.Get(p => p.Code == code);
            if (result.IsActive == false) { return new ErrorResult("link geçersizdir"); }

            bool IsAvailable;
            if (result.EndDate <= DateTime.Now) { IsAvailable = true; }
            else { IsAvailable = false; }
            if (IsAvailable == false) { return new ErrorResult("Linkin Süresi Geçmiştir."); }
            return new SuccessResult();
        }
        public IResult ConfirmResetCodeWithUserId(ConfirmPasswordResetDto confirmPasswordResetDto)
        {
            var result = _resetPasswordCodeDal.Get(p => p.Code == confirmPasswordResetDto.Code);

            if (result.UserId != confirmPasswordResetDto.UserId) { return new ErrorResult("link geçersizdir"); }

            if (result.IsActive == false) { return new ErrorResult("link geçersizdir"); }

            bool IsAvailable;
            if (result.EndDate > DateTime.Now) { IsAvailable = true; }
            else { IsAvailable = false; }

            if (IsAvailable == false) { return new ErrorResult("Linkin Süresi Geçmiştir."); }
            return new SuccessResult();
        }
        public IResult Delete(ResetPasswordCode resetPasswordCode)
        {
            _resetPasswordCodeDal.Delete(resetPasswordCode);
            return new SuccessResult(Messages.Deleted);
        }
        public IDataResult<List<ResetPasswordCode>> GetAll()
        {
            throw new NotImplementedException();
        }
        public IDataResult<ResetPasswordCode> GetByCode(string resetCode)
        {
            return new SuccessDataResult<ResetPasswordCode>(_resetPasswordCodeDal.Get(p => p.Code == resetCode), true, Messages.Listed);

        }
        public IDataResult<ResetPasswordCode> GetById(int resetCodeID)
        {
            throw new NotImplementedException();
        }
        public IDataResult<ResetPasswordCode> GetByUserId(int userId)
        {
            return new SuccessDataResult<ResetPasswordCode>(_resetPasswordCodeDal.Get(p => p.UserId == userId), true, Messages.Listed);
        }
        public IResult Update(ResetPasswordCode resetPasswordCode)
        {
            _resetPasswordCodeDal.Update(resetPasswordCode);
            return new SuccessResult(Messages.Updated);
        }

    }
}
