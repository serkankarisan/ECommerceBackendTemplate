using AutoMapper;
using Business.Abstract.Shoppings;
using Business.Constants;
using Core.Utilities.Paging;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Shoppings;
using Entities.DTOs.Shoppings;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete.Shoppings
{
    public class BasketItemManager : IBasketItemService
    {
        private IBasketItemDal _basketItemDal;
        private IBasketDal _basketDal;
        private IMapper _mapper;
        public BasketItemManager(IBasketItemDal basketItemDal, IMapper mapper, IBasketDal basketDal)
        {
            _basketItemDal = basketItemDal;
            _mapper = mapper;
            _basketDal = basketDal;
        }
        #region Queries
        public async Task<IDataResult<IPaginate<BasketItem>>> GetAllAsync(int index, int size)
        {
            var result = await _basketItemDal.GetListAsync(index: index, size: size);
            return new SuccessDataResult<IPaginate<BasketItem>>(result, Messages.Listed);
        }
        public async Task<IDataResult<BasketItem>> GetByIdAsync(int id)
        {
            var result = await _basketItemDal.GetAsync(p => p.Id == id);
            return result != null ? new SuccessDataResult<BasketItem>(result, Messages.Listed) : new ErrorDataResult<BasketItem>(result, Messages.NotListed);
        }
        public IDataResult<List<BasketItem>> GetBasketItemsByIdUserId(int userId)
        {
            List<BasketItem> basketItems = _basketItemDal.GetAllWithInclude(
                include:
                    i => i.Include(b => b.Basket),
                filter: p => p.UserId == userId);
            return new SuccessDataResult<List<BasketItem>>(basketItems, Messages.Listed);
        }
        #endregion
        #region Commands
        public async Task<IResult> UpdateAsync(BasketItem basketItem)
        {
            var updatedBasketItem = GetByIdAsync(basketItem.Id);
            if (updatedBasketItem == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            var result = await _basketItemDal.UpdateAsync(basketItem);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        public async Task<IResult> AddAsync(AddBasketItemDto basketItemDto)
        {
            var doesUserHaveBasket = await _basketDal.GetAsync(p => p.UserId == basketItemDto.UserId);
            if (doesUserHaveBasket == null)
            {
                basketItemDto.BasketId = _basketDal.AddAsync(new Basket { UserId = basketItemDto.UserId }).Result.Id;
            }
            else
            {
                basketItemDto.BasketId = doesUserHaveBasket.Id;
            }
            BasketItem basketItem = _mapper.Map<BasketItem>(basketItemDto);
            var result = await _basketItemDal.AddAsync(basketItem);
            return result != null ? new SuccessResult(Messages.AddToBasket) : new ErrorResult(Messages.NotAdded);
        }
        public async Task<IResult> DeleteAsync(int id)
        {
            var deletedBasketItem = await GetByIdAsync(id);
            if (deletedBasketItem == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            var result = await _basketItemDal.DeleteAsync(deletedBasketItem.Data);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        #endregion
        decimal calculateTotalPrice(List<BasketItem> basketItems)
        {
            decimal totalPrice = 0;
            foreach (var item in basketItems)
            {
                totalPrice += item.Quantity * item.Product.Price;
            }
            return totalPrice;
        }
    }
}
