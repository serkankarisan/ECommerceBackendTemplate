using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete.Auth;

namespace DataAccess.Concrete.EntityFramework.Auths
{
    public class EfResetPasswordCodeDal : EfEntityRepositoryBase<ResetPasswordCode, ECommerceContext>, IResetPasswordCodeDal
    {
    }
}

