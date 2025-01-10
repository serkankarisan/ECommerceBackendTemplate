using Core.Entities.Concrete.Auth;
using Core.Utilities.Results;
using Entities.DTOs.Users;

namespace Business.Abstract.Auths
{
    public interface IResetPasswordCodeService
    {
        IResult Add(ResetPasswordCode resetPasswordCode);
        IResult Update(ResetPasswordCode resetPasswordCode);
        IResult Delete(ResetPasswordCode resetPasswordCode);
        IDataResult<List<ResetPasswordCode>> GetAll();
        IDataResult<ResetPasswordCode> GetById(int resetCodeID);
        IDataResult<ResetPasswordCode> GetByCode(string resetCode);
        IDataResult<ResetPasswordCode> GetByUserId(int userId);
        IResult ConfirmResetCode(string code);
        IResult ConfirmResetCodeWithUserId(ConfirmPasswordResetDto confirmPasswordResetDto);
    }
}
