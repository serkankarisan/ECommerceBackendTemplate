using Core.Utilities.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.Categories;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        #region Queries
        IDataResult<Category> Get(int id);
        Task<IDataResult<IPaginate<Category>>> GetAllAsync(int index, int size);
        Task<IDataResult<Category>> GetByCategoryIdAsync(string categoryId);
        Task<IDataResult<List<Category>>> GetChildCategoriesByCategoryId(string categoryId);
        bool CategoryIsExist(string categoryId);
        #endregion
        #region Commands
        IResult AddWithDto(AddCategoryDto addCategoryDto);
        Category AddCategoryWithDto(AddCategoryDto addCategoryDto);
        IResult Add(Category category);
        IResult Update(Category category);
        IResult Delete(int id);
        IResult AddChildCategory(AddChildCategoryDto addChildCategoryDto);
        #endregion
    }
}
