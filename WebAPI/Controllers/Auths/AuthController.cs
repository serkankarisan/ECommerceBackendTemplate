using Business.Abstract.Auths;
using Entities.DTOs.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Auths
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;
        private IUserService _userService;
        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            Core.Utilities.Results.IDataResult<Core.Entities.Concrete.Auth.User> userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            Core.Utilities.Results.IDataResult<Core.Utilities.Security.JWT.AccessToken> result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            Core.Utilities.Results.IResult userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }
            Core.Utilities.Results.IDataResult<Core.Entities.Concrete.Auth.User> registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            Core.Utilities.Results.IDataResult<Core.Utilities.Security.JWT.AccessToken> result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("update")]
        public ActionResult Update(UserForUpdateDto userForUpdate)
        {
            _authService.Update(userForUpdate);
            Core.Utilities.Results.IDataResult<Core.Entities.Concrete.Auth.User> user = _userService.GetById(userForUpdate.UserId);
            Core.Utilities.Results.IDataResult<Core.Utilities.Security.JWT.AccessToken> result = _authService.CreateAccessToken(user.Data); if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("change-password")]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            Core.Utilities.Results.IResult result = _authService.ChangePassword(changePasswordDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("password-reset")]
        public IActionResult PasswordReset(PasswordResetDto passwordResetDto)
        {
            Core.Utilities.Results.IResult result = _authService.PasswordReset(passwordResetDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

