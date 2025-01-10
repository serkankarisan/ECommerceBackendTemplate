using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Entities.DTOs.Products
{
    public class AddProductWithImageDto : IDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string CategoryId { get; set; }
        public IFormFileCollection IFormFiles { get; set; }
    }
}
