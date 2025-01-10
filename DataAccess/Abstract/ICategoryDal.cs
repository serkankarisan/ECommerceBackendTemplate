using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
        #region Queries
        List<Category> GetAllParentCategory();
        int GetAllParentCategoryCount();
        Task<List<Category>> GetChildCategoriesByCategoryId(string categoryId);
        bool CategoryIsExist(string categoryId);
        #endregion
    }
}
