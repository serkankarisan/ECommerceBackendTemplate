using Core.Entities;
using Entities.Concrete.Auth;
using Newtonsoft.Json;

namespace Entities.Concrete.Shoppings
{
    public class Basket : BaseEntity
    {
        public int UserId { get; set; }
        //[JsonIgnore]
        public User User { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}
