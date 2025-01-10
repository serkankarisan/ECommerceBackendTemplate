using Core.DataAccess;
using Entities.Concrete.AddressConcrete;

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
