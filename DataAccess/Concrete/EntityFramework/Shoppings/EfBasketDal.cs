using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete.Shoppings;

namespace DataAccess.Concrete.EntityFramework.Shoppings
{
    public class EfBasketDal : EfEntityRepositoryBase<Basket, ECommerceContext>, IBasketDal
    {
    }
}
