using Core.Utilities.Paging;
using Core.Utilities.Results;
using Entities.Concrete.AddressConcrete;

namespace Business.Abstract.Addresses
{
    public interface ICityService
    {
        #region Queries
        Task<IDataResult<IPaginate<City>>> GetAllAsync(int index, int size);
        Task<IDataResult<City>> GetByIdAsync(int id);
        #endregion
        #region Commands
        Task<IResult> AddAsync(City city);
        Task<IResult> UpdateAsync(City city);
        Task<IResult> DeleteAsync(int id);
        #endregion
    }
}
