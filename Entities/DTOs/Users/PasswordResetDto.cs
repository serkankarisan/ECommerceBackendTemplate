﻿using Core.Entities;

namespace Entities.DTOs.Users
{
    public class PasswordResetDto : IDto
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
        public string Code { get; set; }
    }
}

