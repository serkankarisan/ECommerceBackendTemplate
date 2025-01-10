using Core.Entities;
using Entities.Concrete.Shoppings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.AddressConcrete
{
    public class Address : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int CountyId { get; set; }
        public int District { get; set; }
        public string Street { get; set; }
        public string BuildingNo { get; set; }
        public string ApartmentNo { get; set; }
        public string FullAddress { get; set; }
        public List<Order> Orders { get; set; }

    }
}
