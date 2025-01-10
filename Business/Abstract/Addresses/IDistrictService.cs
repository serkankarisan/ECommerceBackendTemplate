using Core.Utilities.Paging;
using Core.Utilities.Results;
using Entities.Concrete.AddressConcrete;

namespace Business.Abstract.Addresses
{
    public interface IDistrictService
    {
        #region Queries
        Task<IDataResult<IPaginate<District>>> GetAllAsync(int index, int size);
        Task<IDataResult<IPaginate<District>>> GetByCountyIdAsync(int index, int size, string countyId);
        Task<IDataResult<District>> GetByIdAsync(int id);
        IDataResult<List<District>> GetAllFromBussiness();
        IDataResult<List<District>> GetAllFromDal();

        #endregion
        #region Commands
        Task<IResult> AddAsync(District district);
        Task<IResult> UpdateAsync(District district);
        Task<IResult> DeleteAsync(int id);
        #endregion
    }
}
