using Core.Entities;

namespace Entities.DTOs.Products
{
    public class ProductDetailDto : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<string> ProductImages { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
}
