﻿using Business.Abstract.Auths;
using Core.Entities.Concrete.Auth;
using Entities.DTOs.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Auths
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IAuthService _authService;
        private IResetPasswordCodeService _resetPasswordCodeService;
        public UsersController(IUserService userService, IResetPasswordCodeService resetPasswordCodeService, IAuthService authService = null)
        {
            _userService = userService;
            _resetPasswordCodeService = resetPasswordCodeService;
            _authService = authService;
        }
        [HttpPost("add-user")]
        public IActionResult AddUser(User user)
        {
            var result = _userService.Add(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("update-user")]
        public IActionResult UpdateUser(User user)
        {
            var result = _userService.Update(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("delete-user")]
        public IActionResult DeleteUser(User user)
        {
            var result = _userService.Delete(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPut("update-infos")]
        public IActionResult UpdateInfos(User user)
        {
            var result = _userService.UpdateInfos(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("get-by-id")]
        public IActionResult GetById(int id)
        {
            var result = _userService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("get-user-by-email")]
        public IActionResult GetByEmail(string email)
        {
            var result = _userService.GetUserByEmail(email);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("send-password-reset-mail")]
        public IActionResult SendEmail(string email)
        {
            var result = _authService.SendResetCodeMail(email);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("confirm-password-reset-code")]
        public IActionResult ConfirmPasswordResetCode(ConfirmPasswordResetDto confirmPasswordResetDto)
        {
            var result = _resetPasswordCodeService.ConfirmResetCodeWithUserId(confirmPasswordResetDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}