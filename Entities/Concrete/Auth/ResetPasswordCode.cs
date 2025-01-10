using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.Auth
{
    public class ResetPasswordCode : BaseEntity
    {
        public string Code { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

    }
}
