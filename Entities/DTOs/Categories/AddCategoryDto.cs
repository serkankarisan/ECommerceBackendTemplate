using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DTOs.Categories
{
    public class AddCategoryDto
    {
        public string Name { get; set; }
        [NotMapped]
        public string? ParentCategoryId { get; set; }
    }
}
