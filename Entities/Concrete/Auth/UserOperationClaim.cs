using Core.Entities;

namespace Entities.Concrete.Auth
{
    public class UserOperationClaim : BaseEntity
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}