using AutoMapper;
using Business.Abstract.Shoppings;
using Business.Constants;
using Core.Utilities.Paging;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Shoppings;
using Entities.DTOs.Shoppings;

namespace Business.Concrete.Shoppings
{
    public class OrderItemManager : IOrderItemService
    {
        private IOrderItemDal _orderItemDal;
        private IMapper _mapper;
        public OrderItemManager(IOrderItemDal orderItemDal, IMapper mapper)
        {
            _orderItemDal = orderItemDal;
            _mapper = mapper;
        }
        #region Queries
        public async Task<IDataResult<IPaginate<OrderItem>>> GetAllAsync(int index, int size)
        {
            var result = await _orderItemDal.GetListAsync(index: index, size: size);
            return result != null ? new SuccessDataResult<IPaginate<OrderItem>>(result, Messages.Listed) : new ErrorDataResult<IPaginate<OrderItem>>(result, Messages.NotListed);
        }
        public async Task<IDataResult<OrderItem>> GetByIdAsync(int id)
        {
            var result = await _orderItemDal.GetAsync(p => p.Id == id);
            return result != null ? new SuccessDataResult<OrderItem>(result, Messages.Listed) : new ErrorDataResult<OrderItem>(result, Messages.NotListed);
        }
        #endregion
        #region Commands
        public async Task<IResult> UpdateAsync(OrderItem orderItem)
        {
            var updatedOrderItem = GetByIdAsync(orderItem.Id);
            if (updatedOrderItem == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            var result = await _orderItemDal.UpdateAsync(orderItem);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        public async Task<IResult> AddAsync(AddOrderItemDto addOrderItemDto)
        {
            OrderItem orderItem = _mapper.Map<OrderItem>(addOrderItemDto);
            var result = await _orderItemDal.AddAsync(orderItem);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        public async Task<IResult> DeleteAsync(int id)
        {
            var deletedOrderItem = await GetByIdAsync(id);
            if (deletedOrderItem == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            var result = await _orderItemDal.DeleteAsync(deletedOrderItem.Data);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        #endregion
    }
}
