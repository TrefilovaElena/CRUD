using CRUD.BLL.Extensions;
using CRUD.BLL.IRepositories;
using CRUD.BLL.Services.Abstract.Filter;
using CRUD.DAL.Context;
using CRUD.DAL.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRUD.DAL.Repositories.Abstract
{
    public class BaseEntityRepository<TEntity> : IRepository<TEntity>
          where TEntity : class
    {
        private bool _disposed;
        protected AppDbContext Db { get; }

        public BaseEntityRepository(AppDbContext context)
        {
            Db = context;
        }

        DbSet<TEntity> _dbSet;
        protected DbSet<TEntity> DbSet
        {
            get
            {
                if (_dbSet == null)
                {
                    Db.ChangeTracker.LazyLoadingEnabled = false;
                    _dbSet = Db.Set<TEntity>();
                }
                return _dbSet;
            }
        }

        string _tableName;
        protected string TableName
        {
            get
            {
                _tableName ??= GetTableName<TEntity>();
                return _tableName;
            }
        }

        public string GetTableName<T>()
        {
            var entityType = Db.Model.FindEntityType(typeof(T));
            var tableName = entityType?.GetTableName();
            return tableName ?? nameof(T);
        }

        virtual public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity);
            await Db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        virtual public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            return DbSet.Where(predicate);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, Dictionary<string, object> properties, CancellationToken cancellationToken = default)
        {
            Db.Entry(entity).CurrentValues.SetValues(properties);
            await Db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> listUpdate, CancellationToken cancellationToken = default)
        {
            DbSet.UpdateRange(listUpdate);
            await Db.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="selectPredicate"></param>
        /// <returns></returns>
        async public Task<TOut?> WhereAndSelectedAsync<TOut>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOut>> selectPredicate, CancellationToken cancellationToken = default)
        {
            if (predicate != null && selectPredicate != null)
            {
                var query = await DbSet
                                 .Where(predicate)
                                 .Select(selectPredicate)
                                 .FirstOrDefaultAsync(cancellationToken);
                return query;
            }
            return default;
        }

        virtual public async Task<TEntity> UpdateAsync(TEntity item, CancellationToken cancellationToken = default)
        {
            var result = Db.Update(item).Entity;
            await Db.SaveChangesAsync(cancellationToken);
            return result;
        }

        virtual public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            var entity = await DbSet.FirstOrDefaultAsync(predicate, cancellationToken);

            return entity;
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> existEntities, IEnumerable<TEntity> newEntities, CancellationToken cancellationToken = default)
        {
            if (existEntities == null || (!existEntities.Any()))
                return;
            DbSet.RemoveRange(existEntities);
            DbSet.AddRange(newEntities);
            await Db.SaveChangesAsync(cancellationToken);
        }


        public async Task RemoveRangeAsync(IEnumerable<TEntity> deleteEntities, CancellationToken cancellationToken = default)
        {
            DbSet.RemoveRange(deleteEntities);
            await Db.SaveChangesAsync(cancellationToken);
        }
        public async Task RemoveAsync(TEntity deleteEntity, CancellationToken cancellationToken = default)
        {
            DbSet.Remove(deleteEntity);
            await Db.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Применение пагинации
        /// </summary>
        /// <typeparam name="TEntityEx"></typeparam>
        /// <param name="query">Список источника</param>
        /// <param name="skip">Сколько сущностей пропустить</param>
        /// <param name="take">Сколько сущностей отобрать</param>
        /// <returns></returns>
        IQueryable<TEntityEx> ApplyPaging<TEntityEx>(IQueryable<TEntityEx> query, int? skip, int? take)
             where TEntityEx : class
        {
            IQueryable<TEntityEx> result = query;

            if (skip.HasValue)
                result = result.Skip(skip.Value);

            if (take.HasValue)
                result = result.Take(take.Value);

            return result;
        }
        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
                                                       bool disableTracking = true,
                                                       params Expression<Func<TEntity, object>>[] includePaths)
        {
            var query = disableTracking ? DbSetQuery : DbSet;
            query = PrepareQueryWithJoins(query, includePaths);
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter,
                                               params Expression<Func<TEntity, object>>[] propertiesToJoin)
        {
            var query = PrepareQueryWithJoins(DbSetQuery, propertiesToJoin);

            if (filter != null)
                query = query.Where(filter);
            return await query.CountAsync();
        }


        public async Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>>? filter,
                                                 int? skip,
                                                 int? take,
                                                 IEnumerable<SortProperty<TEntity>>? sortProperties = null,
                                                 bool disableTracking = true,
                                                 params Expression<Func<TEntity, object>>[] propertiesToJoin)
        {
            var source = disableTracking ? DbSetQuery : DbSet;
            var query = PrepareQueryWithJoins(source, propertiesToJoin);

            if (filter != null)
                query = query.Where(filter);

            var orderedQuery = GetOrderedQuery(query, sortProperties);

            var resultItems = ApplyPaging(orderedQuery, skip, take);

            return await resultItems.ToListAsync(default);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? filter,
                                             params Expression<Func<TEntity, object>>[] propertiesToJoin)
        {
            var query = PrepareQueryWithJoins(DbSetQuery, propertiesToJoin);

            if (filter != null)
                query = query.Where(filter);

            return await query.AnyAsync();
        }


        public async Task<TEntity?> GetByIdAsync(long id,
                                                 bool disableTracking = true,
                                                 params Expression<Func<TEntity, object>>[] propertiesToJoin)
        {
            var query = disableTracking ? DbSetQuery : DbSet;
            query = PrepareQueryWithJoins(query, propertiesToJoin);
            return await query.FirstOrDefaultAsync(e => EF.Property<long>(e, "Id") == id, default);
        }

        protected IQueryable<TEntity> PrepareQueryWithJoins(IQueryable<TEntity> query,
                                                            params Expression<Func<TEntity, object>>[] propertiesToJoin)
        {
            if (propertiesToJoin == null)
                return query;


            foreach (var propertyToJoin in propertiesToJoin)
            {
                var properties = BLL.Extensions.ExpressionExtensions.AsPath(propertyToJoin);
                query = query.Include(properties);
            }


            return query;
        }
        protected IOrderedQueryable<TEntity> GetOrderedQuery(IQueryable<TEntity> query,
                                                             IEnumerable<SortProperty<TEntity>>? sortProperties)
        {

            IOrderedQueryable<TEntity> orderedQuery = query.OrderBy(p => 0);
            if (sortProperties != null && sortProperties.Any())
            {
                var firstSortProperty = true;
                foreach (var sortProperty in sortProperties
                                            .Where(x=> x.Expression != null)
                                            .OrderBy(x=> x.Order))
                {
                    if (firstSortProperty)
                    {
                        orderedQuery = orderedQuery.OrderByWithSortDirection(sortProperty.Expression, sortProperty.SortDirection);
                        firstSortProperty = false;
                    }
                    else
                        orderedQuery = orderedQuery.ThenByWithSortDirection(sortProperty.Expression, sortProperty.SortDirection);
                }
            }
            return orderedQuery;
        }



        public IQueryable<TEntity> DbSetQuery => DbSet.AsNoTracking();

        #region IDisposable
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Db?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }



}
