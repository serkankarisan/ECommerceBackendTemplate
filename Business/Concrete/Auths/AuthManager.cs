using Business.Abstract.Auths;
using Business.Constants;
using Business.Utilities.JWT;
using Business.Utilities.Mail;
using Business.ValidationRules.FluentValidations;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.Concrete.Auth;
using Entities.Concrete.Shoppings;
using Entities.DTOs.Users;

namespace Business.Concrete.Auths
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IResetPasswordCodeService _resetPasswordCodeService;
        private IMailService _mailService;
        private IBasketDal _basketDal;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IResetPasswordCodeService resetPasswordCodeService, IMailService mailService, IBasketDal basketDal)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _resetPasswordCodeService = resetPasswordCodeService;
            _mailService = mailService;
            _basketDal = basketDal;
        }
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var result = _userService.Add(user);
            if (result.Success)
            {
                _basketDal.Add(new Basket { UserId = user.Id });
            }
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, true, Messages.SuccessfulLogin);
        }
        public IDataResult<UserForUpdateDto> Update(UserForUpdateDto userForUpdate)
        {
            var currentCustomer = _userService.GetById(userForUpdate.UserId);

            var user = new User
            {
                Id = userForUpdate.UserId,
                Email = userForUpdate.Email,
                FirstName = userForUpdate.FirstName,
                LastName = userForUpdate.LastName,

            };

            _userService.Update(user);

            return new SuccessDataResult<UserForUpdateDto>(userForUpdate, "Müşteri Güncellendi.");
        }
        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
        public IResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            byte[] passwordHash, passwordSalt;
            var userToCheck = _userService.GetById(changePasswordDto.UserId).Data;
            if (userToCheck == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(changePasswordDto.OldPassword, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorResult(Messages.PasswordError);
            }
            HashingHelper.CreatePasswordHash(changePasswordDto.NewPassword, out passwordHash, out passwordSalt);
            userToCheck.PasswordHash = passwordHash;
            userToCheck.PasswordSalt = passwordSalt;
            _userService.Update(userToCheck);
            return new SuccessResult("Parola Değişti.");
        }
        public IResult PasswordReset(PasswordResetDto passwordResetDto)
        {
            _resetPasswordCodeService.ConfirmResetCode(passwordResetDto.Code);
            var resetPassword = _resetPasswordCodeService.GetByCode(passwordResetDto.Code);
            resetPassword.Data.IsActive = false;

            byte[] passwordHash, passwordSalt;
            var userToCheck = _userService.GetById(passwordResetDto.UserId).Data;
            if (userToCheck == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            HashingHelper.CreatePasswordHash(passwordResetDto.NewPassword, out passwordHash, out passwordSalt);
            userToCheck.PasswordHash = passwordHash;
            userToCheck.PasswordSalt = passwordSalt;
            _userService.Update(userToCheck);
            _resetPasswordCodeService.Update(resetPassword.Data);
            return new SuccessResult("Parola Değişti.");
        }
        public IResult SendResetCodeMail(string email)
        {
            string code = Guid.NewGuid().ToString();

            User user = _userService.GetUserByEmail(email).Data;

            if (user == null) { return new ErrorResult("Kullanıcı Bulunamadı!"); }

            ResetPasswordCode resetPasswordCode = new ResetPasswordCode
            {
                Code = code,
                IsActive = true,
                UserEmail = email,
                CreatedAt = DateTime.Now,
                EndDate = DateTime.Now.AddHours(3),
                UserId = user.Id
            };
            if (_resetPasswordCodeService.Add(resetPasswordCode).Success)
            {
                var result = _mailService.SendPasswordResetMailAsync(resetPasswordCode);
                if (result.Success) { return new SuccessResult("Mail Gönderilmiştir."); }
            };
            return new ErrorResult("Bir hata oluştu");
        }
    }
}