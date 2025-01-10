using Core.Utilities.Security.JWT;
using Entities.Concrete.Auth;

namespace Business.Utilities.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
