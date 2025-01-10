using AutoMapper;
using Business.Abstract.Shoppings;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Paging;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.Shoppings;
using Entities.DTOs.Shoppings;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete.Shoppings
{
    public class BasketManager : IBasketService
    {
        private IBasketDal _basketDal;
        private IMapper _mapper;
        public BasketManager(IBasketDal basketDal, IMapper mapper)
        {
            _basketDal = basketDal;
            _mapper = mapper;
        }
        #region Queries
        public async Task<IDataResult<IPaginate<Basket>>> GetAllAsync(int index, int size)
        {
            Paginate<Basket> result = (Paginate<Basket>)await _basketDal.GetListAsync(
                index: index, size: size,
                include: i => i.Include(u => u.User).Include(b => b.Items).ThenInclude(p => p.Product)
                );

            var a = GeneralExtensions.ClearCircularReference<Paginate<Basket>>(result);

            return result != null ? new SuccessDataResult<IPaginate<Basket>>(a, Messages.Listed) : new ErrorDataResult<IPaginate<Basket>>(a, Messages.NotListed);
        }
        public async Task<IDataResult<Basket>> GetByIdAsync(int id)
        {
            var result = await _basketDal.GetAsync(p => p.Id == id);
            return result != null ? new SuccessDataResult<Basket>(result, Messages.Listed) : new ErrorDataResult<Basket>(result, Messages.NotListed);
        }
        #endregion
        #region Commands
        public async Task<IResult> UpdateAsync(Basket basket)
        {
            var updatedBasket = GetByIdAsync(basket.Id);
            if (updatedBasket == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            var result = await _basketDal.UpdateAsync(basket);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        public async Task<IResult> AddAsync(AddBasketDto addBasketDto)
        {
            Basket basket = _mapper.Map<Basket>(addBasketDto);
            var result = await _basketDal.AddAsync(basket);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        public async Task<IResult> DeleteAsync(int id)
        {
            var deletedBasket = await GetByIdAsync(id);
            if (deletedBasket == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            var result = await _basketDal.DeleteAsync(deletedBasket.Data);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        #endregion
    }
}
