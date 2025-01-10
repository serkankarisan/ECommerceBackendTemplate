using Business.Abstract.Addresses;
using Business.Constants;
using Core.Utilities.Paging;
using Core.Utilities.Results;
using DataAccess.Abstract.AddressAbstract;
using Entities.Concrete.AddressConcrete;
using System.Reflection.Metadata.Ecma335;

namespace Business.Concrete.Addresses
{
    public class CountyManager : ICountyService
    {
        private readonly ICountyDal _countyDal;
        public CountyManager(ICountyDal countyDal)
        {
            _countyDal = countyDal;
        }
        #region Queries
        public async Task<IDataResult<IPaginate<County>>> GetAllAsync(int index, int size)
        {
            var result = await _countyDal.GetListAsync(index: index, size: size);
            return new SuccessDataResult<IPaginate<County>>(result, Messages.Listed);
        }
        public async Task<IDataResult<County>> GetByIdAsync(int id)
        {
            var result = await _countyDal.GetAsync(p => p.Id == id);
            return result != null ? new SuccessDataResult<County>(result, Messages.Listed) : new ErrorDataResult<County>(result, Messages.NotListed);
        }
        public async Task<IDataResult<IPaginate<County>>> GetCountyByCityIdAsync(int index, int size, int cityId)
        {
            var resut = await _countyDal.GetListAsync(index: index, size: size, predicate: p => p.CityId == cityId);
            return resut.Count > 0 ? new SuccessDataResult<IPaginate<County>>(resut, Messages.Listed) : new ErrorDataResult<IPaginate<County>>(resut, Messages.NotListed);
        }
        #endregion
        #region Commands
        public async Task<IResult> UpdateAsync(County county)
        {
            var updatedAddress = await _countyDal.GetAsync(p => p.Id == county.Id);
            if (updatedAddress == null)
                return new ErrorResult(Messages.NotFound);

            var result = await _countyDal.UpdateAsync(updatedAddress);
            return result != null ? new SuccessResult(Messages.Updated) : new ErrorResult(Messages.NotUpdated);
        }
        public async Task<IResult> AddAsync(County county)
        {
            var result = await _countyDal.AddAsync(county);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        public async Task<IResult> DeleteAsync(int id)
        {
            var deletedCounty = await _countyDal.GetAsync(p => p.Id == id);
            if (deletedCounty == null)
                return new ErrorResult(Messages.NotFound);

            var result = await _countyDal.DeleteAsync(deletedCounty);
            return result != null ? new SuccessResult(Messages.Deleted) : new ErrorResult(Messages.NotDeleted);
        }
        #endregion
    }
}
