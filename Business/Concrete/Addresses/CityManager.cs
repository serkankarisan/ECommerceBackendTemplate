using Business.Abstract.Addresses;
using Business.Constants;
using Core.Utilities.Paging;
using Core.Utilities.Results;
using DataAccess.Abstract.AddressAbstract;
using Entities.Concrete.AddressConcrete;

namespace Business.Concrete.Addresses
{
    public class CityManager : ICityService
    {
        private readonly ICityDal _cityDal;
        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }
        #region Queries
        public async Task<IDataResult<IPaginate<City>>> GetAllAsync(int index, int size)
        {
            IPaginate<City>? result = await _cityDal.GetListAsync(index: index, size: size);
            return result != null ? new SuccessDataResult<IPaginate<City>>(result, Messages.Listed) : new ErrorDataResult<IPaginate<City>>(result, Messages.NotListed);
        }
        public async Task<IDataResult<City>> GetByIdAsync(int id)
        {
            City? result = await _cityDal.GetAsync(p => p.Id == id);
            return result != null ? new SuccessDataResult<City>(result, Messages.Listed) : new ErrorDataResult<City>(result, Messages.NotListed);
        }
        #endregion
        #region Commands
        public async Task<IResult> UpdateAsync(City city)
        {
            City? updatedAddress = await _cityDal.GetAsync(p => p.Id == city.Id);
            if (updatedAddress == null)
                return new ErrorResult(Messages.NotFound);

            City result = await _cityDal.UpdateAsync(updatedAddress);
            return result != null ? new SuccessResult(Messages.Updated) : new ErrorResult(Messages.NotUpdated);
        }
        public async Task<IResult> AddAsync(City city)
        {
            City result = await _cityDal.AddAsync(city);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        public async Task<IResult> DeleteAsync(int id)
        {
            City? deletedCity = await _cityDal.GetAsync(p => p.Id == id);
            if (deletedCity == null)
                return new ErrorResult(Messages.NotFound);

            City result = await _cityDal.DeleteAsync(deletedCity);
            return result != null ? new SuccessResult(Messages.Deleted) : new ErrorResult(Messages.NotDeleted);
        }
        #endregion
    }
}
