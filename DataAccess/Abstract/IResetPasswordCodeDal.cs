﻿using Core.DataAccess;
using Core.Entities.Concrete.Auth;

namespace DataAccess.Abstract
{
    public interface IResetPasswordCodeDal : IEntityRepository<ResetPasswordCode>
    {
    }
}
