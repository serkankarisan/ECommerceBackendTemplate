using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Categories
{
    public class AddCategoryDto
    {
        public string Name { get; set; }
        [NotMapped]
        public string? ParentCategoryId { get; set; }
    }
}
