using Core.Utilities.Paging;
using Core.Utilities.Results;
using Entities.Concrete.AddressConcrete;

namespace Business.Abstract.Addresses
{
    public interface ICountyService
    {
        #region Queries
        Task<IDataResult<IPaginate<County>>> GetAllAsync(int index, int size);
        Task<IDataResult<County>> GetByIdAsync(int id);
        Task<IDataResult<IPaginate<County>>> GetCountyByCityIdAsync(int index, int size, int cityId);
        #endregion
        #region Commands
        Task<IResult> AddAsync(County county);
        Task<IResult> UpdateAsync(County county);
        Task<IResult> DeleteAsync(int id);
        #endregion
    }
}
