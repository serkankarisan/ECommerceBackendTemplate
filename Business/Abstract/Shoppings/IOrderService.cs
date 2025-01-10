using Core.Utilities.Paging;
using Core.Utilities.Results;
using Entities.Concrete.Shoppings;
using Entities.DTOs.Shoppings;

namespace Business.Abstract.Shoppings
{
    public interface IOrderService
    {
        Task<IResult> AddAsync(AddOrderDto addOrderDto);
        Task<IResult> UpdateAsync(Order order);
        Task<IResult> DeleteAsync(int id);
        Task<IDataResult<IPaginate<Order>>> GetAllAsync(int index, int size);
        Task<IDataResult<Order>> GetByIdAsync(int id);
    }
}
