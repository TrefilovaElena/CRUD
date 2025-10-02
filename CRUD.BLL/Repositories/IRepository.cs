using CRUD.BLL.Services.Abstract.Filter;
using System.Linq.Expressions;

namespace CRUD.BLL.IRepositories
{
    public interface IRepository<T> : IDisposable where T : class
    {

        Task<T?> GetByIdAsync(long id, bool disableTracking = true,
                                       params Expression<Func<T, object>>[] propertiesToJoin);

        Task<int> CountAsync(Expression<Func<T, bool>>? filter,
                                               params Expression<Func<T, object>>[] propertiesToJoin);
        Task<List<T>> FilterAsync(Expression<Func<T, bool>>? filter,
                                  int? skip = null,
                                  int? take = null,
                                  IEnumerable<SortProperty<T>>? sortProperties = null,
                                  bool disableTracking = true,
                                  params Expression<Func<T, object>>[] propertiesToJoin);

        Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null,
                            params Expression<Func<T, object>>[] propertiesToJoin);

        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate,
                                                       bool disableTracking = true,
                                                       params Expression<Func<T, object>>[] includePaths);

        Task<T> CreateAsync(T item, CancellationToken cancellationToken = default);

        /// <summary>
        /// Обновление сущности
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T item, CancellationToken cancellationToken = default);
        Task<T> UpdateAsync(T item, Dictionary<string, object> properties, CancellationToken cancellationToken = default);
        Task UpdateRangeAsync(IEnumerable<T> existEntities, IEnumerable<T> newEntities, CancellationToken cancellationToken = default);
        Task UpdateRangeAsync(IEnumerable<T> listUpdate, CancellationToken cancellationToken = default);
        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="id"></param>
        //Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);

        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        Task<TOut?> WhereAndSelectedAsync<TOut>(Expression<Func<T, bool>> predicate, Expression<Func<T, TOut>> selectPredicate, CancellationToken cancellationToken = default);

        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task RemoveRangeAsync(IEnumerable<T> deleteEntities, CancellationToken cancellationToken = default);

        Task RemoveAsync(T delete, CancellationToken cancellationToken = default);


    }
}
