using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.Auth;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework.Auths
{
    public class EfUserDal : EfEntityRepositoryBase<User, ECommerceContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (ECommerceContext context = new ECommerceContext())
            {
                IQueryable<OperationClaim> result = from operationClaim in context.OperationClaims
                                                    join userOperationClaim in context.UserOperationClaims
                                                        on operationClaim.Id equals userOperationClaim.OperationClaimId
                                                    where userOperationClaim.UserId == user.Id
                                                    select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }
    }
}
