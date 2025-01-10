using Core.Utilities.Paging;
using Core.Utilities.Results;
using Entities.Concrete.Shoppings;
using Entities.DTOs.Shoppings;

namespace Business.Abstract.Shoppings
{
    public interface IBasketService
    {
        Task<IResult> AddAsync(AddBasketDto addBasketDto);
        Task<IResult> UpdateAsync(Basket brand);
        Task<IResult> DeleteAsync(int id);
        Task<IDataResult<IPaginate<Basket>>> GetAllAsync(int index, int size);
        Task<IDataResult<Basket>> GetByIdAsync(int id);
    }
}
