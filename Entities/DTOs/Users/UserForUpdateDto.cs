﻿using Core.Entities;

namespace Entities.DTOs.Users
{
    public class UserForUpdateDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
    }
}
