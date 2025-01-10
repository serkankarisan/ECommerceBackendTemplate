using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete.AddressConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.AddressAbstract
{
    public interface ICountryDal : IEntityRepository<Country>
    {

    }
    public interface ICityDal : IEntityRepository<City>
    {

    }
    public interface ICountyDal : IEntityRepository<County>
    {

    }
    public interface IDistrictDal : IEntityRepository<District>
    {
        List<District> GetAllFromDal();
    }
    public interface IAddressDal : IEntityRepository<Address>
    {

    }
}
