﻿using Core.Entities;
using Entities.Concrete.AddressConcrete;
using Entities.Concrete.Auth;

namespace Entities.Concrete.Shoppings
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public Basket Basket{ get; set; }
    }
}