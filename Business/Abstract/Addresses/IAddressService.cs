using Core.Utilities.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Concrete.AddressConcrete;
using Entities.DTOs.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.Addresses
{
    public interface IAddressService
    {
        #region Queries
        Task<IDataResult<IPaginate<Address>>> GetAllAsync(int index, int size);
        Task<IDataResult<Address>> GetByIdAsync(int id);
        Task<IDataResult<Address>> GetByUserIdAsync(int userId);
        Task<IDataResult<IPaginate<Address>>> GetAllByUserIdAsync(int index, int size, int userId);
        #endregion
        #region Commands
        Task<IResult> AddAsync(AddAddressDto addAddressDto);
        Task<IResult> UpdateAsync(Address address);
        Task<IResult> DeleteAsync(int id);
        #endregion
    }
}
