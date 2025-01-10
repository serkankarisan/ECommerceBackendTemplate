using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete.Shoppings;

namespace DataAccess.Concrete.EntityFramework.Shoppings
{
    public class EfBasketItemDal : EfEntityRepositoryBase<BasketItem, ECommerceContext>, IBasketItemDal
    {
    }
}
