using Core.Utilities.Paging;
using Core.Utilities.Results;
using Entities.Concrete.Shoppings;
using Entities.DTOs.Shoppings;

namespace Business.Abstract.Shoppings
{
    public interface IBasketItemService
    {
        Task<IResult> AddAsync(AddBasketItemDto basketItemDto);
        Task<IResult> UpdateAsync(BasketItem basketItem);
        Task<IResult> DeleteAsync(int id);
        Task<IDataResult<IPaginate<BasketItem>>> GetAllAsync(int index, int size);
        Task<IDataResult<BasketItem>> GetByIdAsync(int id);
        IDataResult<List<BasketItem>> GetBasketItemsByIdUserId(int userId);

    }
}
