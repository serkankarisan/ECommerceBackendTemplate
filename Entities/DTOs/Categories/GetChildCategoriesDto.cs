using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Categories
{
    public class GetChildCategoriesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public List<GetChildCategoriesDto> ChildCategories { get; set; }
    }
}
