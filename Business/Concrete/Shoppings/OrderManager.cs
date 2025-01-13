using Business.Abstract.Shoppings;
using Business.Constants;
using Core.Utilities.Paging;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Shoppings;
using Entities.DTOs.Shoppings;

namespace Business.Concrete.Shoppings
{
    public class OrderManager : IOrderService
    {
        private IOrderDal _orderDal;
        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        #region Queries
        public async Task<IDataResult<IPaginate<Order>>> GetAllAsync(int index, int size)
        {
            IPaginate<Order>? result = await _orderDal.GetListAsync(index: index, size: size);
            return result != null ? new SuccessDataResult<IPaginate<Order>>(result, Messages.Listed) : new ErrorDataResult<IPaginate<Order>>(result, Messages.NotListed);
        }
        public async Task<IDataResult<Order>> GetByIdAsync(int id)
        {
            Order? result = await _orderDal.GetAsync(p => p.Id == id);
            return result != null ? new SuccessDataResult<Order>(result, Messages.Listed) : new ErrorDataResult<Order>(result, Messages.NotListed);
        }
        #endregion
        #region Commands
        public async Task<IResult> UpdateAsync(Order order)
        {
            Task<IDataResult<Order>> updatedOrder = GetByIdAsync(order.Id);
            if (updatedOrder == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            Order result = await _orderDal.UpdateAsync(order);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        public async Task<IResult> AddAsync(Order order)
        {
            Order result = await _orderDal.AddAsync(order);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        public async Task<IResult> DeleteAsync(int id)
        {
            IDataResult<Order> deletedOrder = await GetByIdAsync(id);
            if (deletedOrder == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            Order result = await _orderDal.DeleteAsync(deletedOrder.Data);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }

        public Task<IResult> AddAsync(AddOrderDto addOrderDto)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
