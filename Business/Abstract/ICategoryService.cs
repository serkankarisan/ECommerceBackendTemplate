using Core.Utilities.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.Categories;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        #region Queries
        IDataResult<CategoryDto> Get(Expression<Func<Category, bool>> filter);
        Task<IDataResult<Paginate<CategoryDto>>> GetAllAsync(int index, int size);
        Task<IDataResult<CategoryDto>> GetByCategoryIdAsync(int categoryId);
        Task<IDataResult<List<CategoryDto>>> GetChildCategoriesByCategoryId(int categoryId);
        bool CategoryIsExist(string name);
        #endregion
        #region Commands
        IDataResult<CategoryDto> AddWithDto(AddCategoryDto addCategoryDto);
        IResult Add(Category category);
        IResult Update(Category category);
        IResult Delete(int id);
        #endregion
    }
}
