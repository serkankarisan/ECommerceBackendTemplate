using AutoMapper;
using Business.Abstract.Addresses;
using Business.Constants;
using Core.Utilities.Paging;
using Core.Utilities.Results;
using DataAccess.Abstract.AddressAbstract;
using Entities.Concrete.AddressConcrete;
using Entities.DTOs.Addresses;

namespace Business.Concrete.Addresses
{
    public class AddressManager : IAddressService
    {
        private readonly IAddressDal _addressDal;
        private readonly IMapper _mapper;
        public AddressManager(IAddressDal addressDal, IMapper mapper)
        {
            _addressDal = addressDal;
            _mapper = mapper;
        }
        #region Queries
        public async Task<IDataResult<IPaginate<Address>>> GetAllAsync(int index, int size)
        {
            IPaginate<Address>? result = await _addressDal.GetListAsync(index: index, size: size);
            return result != null ? new SuccessDataResult<IPaginate<Address>>(result, Messages.Listed) : new ErrorDataResult<IPaginate<Address>>(result, Messages.NotListed);
        }
        public async Task<IDataResult<IPaginate<Address>>> GetAllByUserIdAsync(int index, int size, int userId)
        {
            IPaginate<Address> result = await _addressDal.GetListAsync(index: index, size: size, predicate: p => p.UserId == userId);
            return result.Count != 0 ? new SuccessDataResult<IPaginate<Address>>(result, Messages.Listed) : new ErrorDataResult<IPaginate<Address>>(result, Messages.NotListed);
        }
        public async Task<IDataResult<Address>> GetByIdAsync(int id)
        {
            Address? result = await _addressDal.GetAsync(p => p.Id == id);
            return result != null ? new SuccessDataResult<Address>(result, Messages.Listed) : new ErrorDataResult<Address>(result, Messages.NotListed);
        }
        public async Task<IDataResult<Address>> GetByUserIdAsync(int userId)
        {
            Address? result = await _addressDal.GetAsync(p => p.UserId == userId);
            return result != null ? new SuccessDataResult<Address>(result, Messages.Listed) : new ErrorDataResult<Address>(result, Messages.NotListed);
        }
        #endregion
        #region Commands
        public async Task<IResult> UpdateAsync(Address address)
        {
            Address? updatedAddress = await _addressDal.GetAsync(p => p.Id == address.Id);
            if (updatedAddress == null)
                return new ErrorResult(Messages.NotFound);

            Address result = await _addressDal.UpdateAsync(updatedAddress);
            return result != null ? new SuccessResult(Messages.Updated) : new ErrorResult(Messages.NotUpdated);
        }
        public async Task<IResult> AddAsync(AddAddressDto addAddressDto)
        {
            Address address = _mapper.Map<Address>(addAddressDto);
            Address result = await _addressDal.AddAsync(address);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        public async Task<IResult> DeleteAsync(int id)
        {
            Address? deletedAddress = await _addressDal.GetAsync(p => p.Id == id);
            if (deletedAddress == null)
                return new ErrorResult(Messages.NotFound);

            Address result = await _addressDal.DeleteAsync(deletedAddress);
            return result != null ? new SuccessResult(Messages.Deleted) : new ErrorResult(Messages.NotDeleted);
        }
        #endregion
    }
}
