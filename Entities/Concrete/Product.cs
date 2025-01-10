using Core.Entities;
using Entities.Concrete.Shoppings;

namespace Entities.Concrete
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string CategoryId { get; set; }
        public int BrandId { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
