using Core.Entities;

namespace Entities.DTOs.Products
{
    public class AddProductDto : IDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string CategoryId { get; set; }
        public int BrandId { get; set; }
    }
}
