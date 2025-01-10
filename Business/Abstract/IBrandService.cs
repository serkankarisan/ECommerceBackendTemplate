using Core.Utilities.Paging;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IBrandService
    {
        Task<IResult> AddAsync(Brand brand);
        Task<IResult> UpdateAsync(Brand brand);
        Task<IResult> DeleteAsync(int id);
        Task<IDataResult<IPaginate<Brand>>> GetAllAsync(int index, int size);
        Task<IDataResult<Brand>> GetByIdAsync(int id);
    }
}
