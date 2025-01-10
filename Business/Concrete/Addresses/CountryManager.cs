using Business.Abstract.Addresses;
using Business.Constants;
using Core.Utilities.Paging;
using Core.Utilities.Results;
using DataAccess.Abstract.AddressAbstract;
using Entities.Concrete.AddressConcrete;

namespace Business.Concrete.Addresses
{
    public class CountryManager : ICountryService
    {
        private readonly ICountryDal _countryDal;
        public CountryManager(ICountryDal countryDal)
        {
            _countryDal = countryDal;
        }
        #region Queries
        public async Task<IDataResult<IPaginate<Country>>> GetAllAsync(int index, int size)
        {
            var result = await _countryDal.GetListAsync(index: index, size: size);
            return result != null ? new SuccessDataResult<IPaginate<Country>>(result, Messages.Listed) : new ErrorDataResult<IPaginate<Country>>(result, Messages.NotListed);
        }
        public async Task<IDataResult<Country>> GetByIdAsync(int id)
        {
            var result = await _countryDal.GetAsync(p => p.Id == id);
            return result != null ? new SuccessDataResult<Country>(result, Messages.Listed) : new ErrorDataResult<Country>(result, Messages.NotListed);
        }
        #endregion
        #region Commands
        public async Task<IResult> UpdateAsync(Country country)
        {
            var updatedAddress = await _countryDal.GetAsync(p => p.Id == country.Id);
            if (updatedAddress == null)
                return new ErrorResult(Messages.NotFound);

            var result = await _countryDal.UpdateAsync(updatedAddress);
            return result != null ? new SuccessResult(Messages.Updated) : new ErrorResult(Messages.NotUpdated);
        }
        public async Task<IResult> AddAsync(Country country)
        {
            var result = await _countryDal.AddAsync(country);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        public async Task<IResult> DeleteAsync(int id)
        {
            var deletedCountry = await _countryDal.GetAsync(p => p.Id == id);
            if (deletedCountry == null)
                return new ErrorResult(Messages.NotFound);

            var result = await _countryDal.DeleteAsync(deletedCountry);
            return result != null ? new SuccessResult(Messages.Deleted) : new ErrorResult(Messages.NotDeleted);
        }
        #endregion
    }
}
