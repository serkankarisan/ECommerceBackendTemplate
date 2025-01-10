using Core.Utilities.Results;
using Entities.Concrete.Auth;


namespace Business.Abstract.Auths
{
    public interface IUserService
    {
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
        IResult UpdateInfos(User user);
        List<OperationClaim> GetClaims(User user);
        IDataResult<User> GetUserByEmail(string email);
        User GetByMail(string email);
        IDataResult<User> GetById(int id);
    }
}
