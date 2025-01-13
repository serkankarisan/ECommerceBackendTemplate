using Business.Abstract.Addresses;
using Business.Constants;
using Core.Utilities.Paging;
using Core.Utilities.Results;
using DataAccess.Abstract.AddressAbstract;
using Entities.Concrete.AddressConcrete;

namespace Business.Concrete.Addresses
{
    public class DistrictManager : IDistrictService
    {
        private readonly IDistrictDal _districtDal;
        public DistrictManager(IDistrictDal districtDal)
        {
            _districtDal = districtDal;
        }
        #region Queries
        public async Task<IDataResult<IPaginate<District>>> GetAllAsync(int index, int size)
        {
            IPaginate<District>? result = await _districtDal.GetListAsync(index: index, size: size);
            return result != null ? new SuccessDataResult<IPaginate<District>>(result, Messages.Listed) : new ErrorDataResult<IPaginate<District>>(result, Messages.NotListed);
        }
        public async Task<IDataResult<District>> GetByIdAsync(int id)
        {
            District? result = await _districtDal.GetAsync(p => p.Id == id);
            return result != null ? new SuccessDataResult<District>(result, Messages.Listed) : new ErrorDataResult<District>(result, Messages.NotListed);
        }
        public async Task<IDataResult<IPaginate<District>>> GetByCountyIdAsync(int index, int size, string countyId)
        {
            IPaginate<District>? result = await _districtDal.GetListAsync(index: index, size: size, predicate: p => p.CountyId == countyId);
            return result != null ? new SuccessDataResult<IPaginate<District>>(result) : new ErrorDataResult<IPaginate<District>>(result);
        }
        #endregion
        #region Commands
        public async Task<IResult> UpdateAsync(District district)
        {
            District? updatedAddress = await _districtDal.GetAsync(p => p.Id == district.Id);
            if (updatedAddress == null)
                return new ErrorResult(Messages.NotFound);

            District result = await _districtDal.UpdateAsync(updatedAddress);
            return result != null ? new SuccessResult(Messages.Updated) : new ErrorResult(Messages.NotUpdated);
        }
        public async Task<IResult> AddAsync(District district)
        {
            District result = await _districtDal.AddAsync(district);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        public async Task<IResult> DeleteAsync(int id)
        {
            District? deletedDistrict = await _districtDal.GetAsync(p => p.Id == id);
            if (deletedDistrict == null)
                return new ErrorResult(Messages.NotFound);

            District result = await _districtDal.DeleteAsync(deletedDistrict);
            return result != null ? new SuccessResult(Messages.Deleted) : new ErrorResult(Messages.NotDeleted);
        }

        public IDataResult<List<District>> GetAllFromBussiness()
        {
            List<District> result = _districtDal.GetAll().Where(p => p.Id % 2 == 0).ToList();
            return new SuccessDataResult<List<District>>(result, Messages.Listed);
        }

        public IDataResult<List<District>> GetAllFromDal()
        {
            List<District> result = _districtDal.GetAllFromDal();
            return new SuccessDataResult<List<District>>(result, Messages.Listed);
        }
        #endregion
    }
}
