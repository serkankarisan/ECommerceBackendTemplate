﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Entities.Concrete.Auth;

namespace DataAccess.Abstract
{
    public interface IResetPasswordCodeDal: IEntityRepository<ResetPasswordCode>
    {
    }
}
