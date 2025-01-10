using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.Concrete.Auth;
using Entities.DTOs.Users;

namespace Business.Abstract.Auths
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IDataResult<UserForUpdateDto> Update(UserForUpdateDto userForUpdate);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IResult ChangePassword(ChangePasswordDto changePasswordDto);
        IResult PasswordReset(PasswordResetDto passwordResetDto);
        IResult SendResetCodeMail(string email);
    }
}