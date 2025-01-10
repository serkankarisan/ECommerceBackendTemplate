using Core.Utilities.Dynamic;
using Core.Utilities.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class
    {
        List<T> GetAll(Expression<Func<T, bool>>? filter = null);
        List<T> GetAllWithInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Expression<Func<T, bool>>? filter = null);
        T Get(Expression<Func<T, bool>> filter);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        bool IsExist(Expression<Func<T, bool>> filter);
        bool Add(T entity);
        bool AddRange(List<T> entities);
        bool Update(T entity);
        bool Delete(T entity);

        Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>,
                     IIncludableQueryable<T, object>>? include = null, bool enableTracking = true,
                     CancellationToken cancellationToken = default);

        Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                        int index = 0, int size = 10, bool enableTracking = true,
                                        CancellationToken cancellationToken = default);

        Task<IPaginate<T>> GetListByDynamicAsync(Dynamic dynamic,
                                                 Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                                 int index = 0, int size = 10, bool enableTracking = true,
                                                 CancellationToken cancellationToken = default);

        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);

        T Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>,
         IIncludableQueryable<T, object>>? include = null, bool enableTracking = true);

        IPaginate<T> GetList(Expression<Func<T, bool>>? predicate = null,
                             Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                             Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                             int index = 0, int size = 10,
                             bool enableTracking = true);

        IPaginate<T> GetListByDynamic(Dynamic dynamic,
                                      Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                      int index = 0, int size = 10, bool enableTracking = true);

        int GetTableCount(Expression<Func<T, bool>> filter = null);

    }
}
