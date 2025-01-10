using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete.Auth;

namespace DataAccess.Concrete.EntityFramework.Auths
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, ECommerceContext>, IUserOperationClaimDal
    {
    }
}

