using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, ECommerceContext>, ICategoryDal
    {
        public bool CategoryIsExist(string name)
        {
            using (ECommerceContext context = new ECommerceContext())
            {
                return context.Categories.Any(p => p.Name == name);
            }
        }
        #region Queries
        public List<Category> GetAllParentCategory()
        {
            using (ECommerceContext context = new ECommerceContext())
            {
                IQueryable<Category> queryable = context.Set<Category>().AsQueryable();
                return queryable.Where(p => p.SubCategories.Count() > 0).ToList();
            }
        }
        public int GetAllParentCategoryCount()
        {
            using (ECommerceContext context = new ECommerceContext())
            {
                IQueryable<Category> queryable = context.Set<Category>().AsQueryable();
                return queryable.Count(p => p.SubCategories.Count() > 0);
            }
        }
        public async Task<List<Category>> GetChildCategoriesByCategoryId(int categoryId)
        {
            using (ECommerceContext context = new ECommerceContext())
            {
                IQueryable<Category> queryable = context.Set<Category>().AsQueryable();
                bool categoryExists = await queryable.AnyAsync(p => p.Id == categoryId);
                if (categoryExists)
                {
                    return queryable.Where(q => q.ParentCategoryId == categoryId).ToList();
                }
                return new List<Category>();
            }
        }
        #endregion
    }
}
