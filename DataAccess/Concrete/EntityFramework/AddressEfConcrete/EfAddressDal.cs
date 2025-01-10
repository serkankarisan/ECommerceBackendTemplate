using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Abstract.AddressAbstract;
using Entities.Concrete;
using Entities.Concrete.AddressConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.AddressEfConcrete
{
    public class EfAddressDal : EfEntityRepositoryBase<Address, ECommerceContext>, IAddressDal
    {
    }
    public class EfCountryDal : EfEntityRepositoryBase<Country, ECommerceContext>, ICountryDal
    {
    }
    public class EfCountyDal : EfEntityRepositoryBase<County, ECommerceContext>, ICountyDal
    {
    }
    public class EfDistrictDal : EfEntityRepositoryBase<District, ECommerceContext>, IDistrictDal
    {
        public List<District> GetAllFromDal()
        {
            using (var _context = new ECommerceContext())
            {
                List<District> result = _context.Districts.Where(p => p.Id % 2 == 0).ToList();
                return result;
            }
        }
    }
    public class EfCityDal : EfEntityRepositoryBase<City, ECommerceContext>, ICityDal
    {
    }
}
