using Core.Entities;

namespace Entities.Concrete
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }
        public string CategoryId { get; set; }
        //public string? ParentCategoryId { get; set; }
        //public virtual ICollection<Product>? Products { get; set; }
    }
}
