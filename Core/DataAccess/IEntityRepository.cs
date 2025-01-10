using Core.Entities;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> filter);
        IList<T> GetList(Expression<Func<T, bool>> filter = null);
        int Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool IsExists(Expression<Func<T, bool>> filter);
    }
}
