using Core.Entities;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Entities.DTOs.Products
{
    public class ListProductDto : IDto
    {
        public Product Product { get; set; }
        public List<ProductImage> ProductImages { get; set; }

    }
}
