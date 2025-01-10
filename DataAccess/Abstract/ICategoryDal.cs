using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
        #region Queries
        List<Category> GetAllParentCategory();
        int GetAllParentCategoryCount();
        Task<List<Category>> GetChildCategoriesByCategoryId(int categoryId);
        bool CategoryIsExist(string name);
        #endregion
    }
}
