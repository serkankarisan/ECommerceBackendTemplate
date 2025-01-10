using AutoMapper;
using Business.Concrete;
using Entities.Concrete;
using Entities.Concrete.AddressConcrete;
using Entities.Concrete.Shoppings;
using Entities.DTOs.Addresses;
using Entities.DTOs.Categories;
using Entities.DTOs.Products;
using Entities.DTOs.Shoppings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Category Mapping
            CreateMap<AddCategoryDto, Category>().ReverseMap();
            CreateMap<AddChildCategoryDto, Category>().ReverseMap();
            #endregion
            #region Products Mapping
            CreateMap<AddProductDto, Product>().ReverseMap();
            CreateMap<AddProductWithImageDto, Product>().ReverseMap();
            #endregion
            #region Shopping
            CreateMap<AddBasketDto, Basket>().ReverseMap();
            CreateMap<AddBasketItemDto, BasketItem>().ReverseMap();
            CreateMap<AddOrderDto, Order>().ReverseMap();
            CreateMap<AddOrderItemDto, OrderItem>().ReverseMap();
            #endregion

            CreateMap<AddAddressDto, Address>().ReverseMap();

        }
    }
}
