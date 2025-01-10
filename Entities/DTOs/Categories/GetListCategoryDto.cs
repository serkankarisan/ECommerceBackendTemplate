using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Categories
{
    public class GetListCategoryDto : IDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public ICollection<GetListCategoryDto>? ChildCategories { get; set; }
    }
}
