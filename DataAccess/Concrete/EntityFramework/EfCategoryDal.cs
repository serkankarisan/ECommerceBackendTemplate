using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, ECommerceContext>, ICategoryDal
    {
        public bool CategoryIsExist(string categoryId)
        {
            using (ECommerceContext context = new ECommerceContext())
            {
                var a = context.Categories.Count(p => p.CategoryId == categoryId) > 0;
            }
            using (ECommerceContext context = new ECommerceContext())
            {
                var a = context.Categories.Any(p => p.CategoryId == categoryId);
            }
            return true;
        }
        #region Queries
        public List<Category> GetAllParentCategory()
        {
            using (ECommerceContext context = new ECommerceContext())
            {
                IQueryable<Category> queryable = context.Set<Category>().AsQueryable();
                return queryable.Where(p => !p.CategoryId.Contains("-")).ToList();
            }
        }
        public int GetAllParentCategoryCount()
        {
            using (ECommerceContext context = new ECommerceContext())
            {
                IQueryable<Category> queryable = context.Set<Category>().AsQueryable();
                return queryable.Where(p => !p.CategoryId.Contains("-")).Count();
            }
        }
        public async Task<List<Category>> GetChildCategoriesByCategoryId(string categoryId)
        {
            using (ECommerceContext context = new ECommerceContext())
            {
                IQueryable<Category> queryable = context.Set<Category>().AsQueryable();
                return await queryable.Where(p => p.CategoryId.StartsWith($"{categoryId}-")).ToListAsync();
            }
        }
        #endregion
    }
}
