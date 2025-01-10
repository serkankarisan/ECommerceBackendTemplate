using Core.Entities;
using Core.Utilities.Dynamic;
using Core.Utilities.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }
        public virtual List<TEntity> GetAllWithInclude(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, Expression<Func<TEntity, bool>>? filter = null)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> queryable = context.Set<TEntity>().AsQueryable();
                queryable = include(queryable);
                return filter == null ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }
        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }
        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().FirstOrDefault(filter);
            }
        }
        public virtual bool Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                return context.SaveChanges() > 0;
            }
        }
        public virtual bool AddRange(List<TEntity> entities)
        {
            using (TContext context = new TContext())
            {
                context.AddRange(entities);
                return context.SaveChanges() > 0;
            }
        }
        public virtual bool Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                return context.SaveChanges() > 0;
            }
        }
        public virtual bool Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                return context.SaveChanges() > 0;
            }
        }

        public IQueryable<TEntity> Query()
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>();
            }
        }

        #region Async Methods
        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>,
                                     IIncludableQueryable<TEntity, object>>? include = null, bool enableTracking = true,
                                     CancellationToken cancellationToken = default)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> queryable = context.Set<TEntity>().AsQueryable();
                if (!enableTracking) queryable = queryable.AsNoTracking();
                if (include != null) queryable = include(queryable);
                return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
            }
        }
        public async Task<IPaginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                                      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                      Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                                      int index = 0, int size = 10, bool enableTracking = true,
                                                      CancellationToken cancellationToken = default)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> queryable = context.Set<TEntity>().AsQueryable();
                if (!enableTracking) queryable = queryable.AsNoTracking();
                if (include != null) queryable = include(queryable);
                if (predicate != null) queryable = queryable.Where(predicate);
                if (orderBy != null)
                    return await orderBy(queryable).ToPaginateAsync(index, size, 0, cancellationToken);
                return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
            }
        }

        public async Task<IPaginate<TEntity>> GetListByDynamicAsync(Dynamic dynamic,
                                                               Func<IQueryable<TEntity>,
                                                                       IIncludableQueryable<TEntity, object>>?
                                                                   include = null,
                                                               int index = 0, int size = 10,
                                                               bool enableTracking = true,
                                                               CancellationToken cancellationToken = default)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> queryable = context.Set<TEntity>().AsQueryable().ToDynamic(dynamic);
                if (!enableTracking) queryable = queryable.AsNoTracking();
                if (include != null) queryable = include(queryable);
                return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
            }
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                context.Entry(entity).State = EntityState.Added;
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                context.Entry(entity).State = EntityState.Deleted;
                await context.SaveChangesAsync();
                return entity;
            }
        }
        #endregion
        #region Normal Methods
        public TEntity Get(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>,
                      IIncludableQueryable<TEntity, object>>? include = null, bool enableTracking = true)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> queryable = context.Set<TEntity>().AsQueryable();
                if (!enableTracking) queryable = queryable.AsNoTracking();
                if (include != null) queryable = include(queryable);
                return queryable.FirstOrDefault(predicate);
            }
        }
        public IPaginate<TEntity> GetList(Expression<Func<TEntity, bool>>? predicate = null,
                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                     Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                     int index = 0, int size = 10,
                                     bool enableTracking = true)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> queryable = context.Set<TEntity>().AsQueryable();
                if (!enableTracking) queryable = queryable.AsNoTracking();
                if (include != null) queryable = include(queryable);
                if (predicate != null) queryable = queryable.Where(predicate);
                if (orderBy != null)
                    return orderBy(queryable).ToPaginate(index, size);
                return queryable.ToPaginate(index, size);
            }
        }
        public IPaginate<TEntity> GetListByDynamic(Dynamic dynamic,
                                                   Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>?
                                                       include = null, int index = 0, int size = 10,
                                                   bool enableTracking = true)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> queryable = context.Set<TEntity>().AsQueryable().ToDynamic(dynamic);
                if (!enableTracking) queryable = queryable.AsNoTracking();
                if (include != null) queryable = include(queryable);
                return queryable.ToPaginate(index, size);
            }
        }

        public int GetTableCount(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> queryable = context.Set<TEntity>().AsQueryable();
                if (filter == null)
                    return queryable.Count();
                return queryable.Count(filter);
            }
        }

        public bool IsExist(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> queryable = context.Set<TEntity>().AsQueryable();
                if (filter == null)
                    return queryable.Any();
                return queryable.Any(filter);
            }
        }
        #endregion
    }
}
