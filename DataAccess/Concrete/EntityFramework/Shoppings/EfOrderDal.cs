using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete.Shoppings;

namespace DataAccess.Concrete.EntityFramework.Shoppings
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, ECommerceContext>, IOrderDal
    {
    }
}
