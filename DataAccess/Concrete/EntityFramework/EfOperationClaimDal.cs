using Core.DataAccess;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Auth;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim, ECommerceContext>, IOperationClaimDal
    {
    }
}

