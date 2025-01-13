using Business.Abstract.Auths;
using Business.Constants;
using Core.Entities.Concrete.Auth;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete.Auths
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.Added);
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.Updated);
        }
        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.Deleted);
        }
        public IResult UpdateInfos(User user)
        {
            User userToUpdate = GetById(user.Id).Data;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Email = user.Email;
            Update(userToUpdate);
            return new SuccessResult();
        }
        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }
        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }
        public IDataResult<User> GetUserByEmail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email), Messages.Listed);
        }
        public IDataResult<User> GetById(int id)
        {
            User result = _userDal.Get(p => p.Id == id);
            return new SuccessDataResult<User>(result);
        }
    }
}
