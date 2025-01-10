using AutoMapper;
using Entities.Concrete;
using Entities.Concrete.AddressConcrete;
using Entities.Concrete.Shoppings;
using Entities.DTOs.Addresses;
using Entities.DTOs.Categories;
using Entities.DTOs.Products;
using Entities.DTOs.Shoppings;

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
