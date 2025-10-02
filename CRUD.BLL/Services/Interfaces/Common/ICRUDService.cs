using System.Linq.Expressions;

namespace CRUD.BLL.Services.Interfaces.Common
{
    public interface ICRUDService<T>
    {
        Task<T?> GetByIdAsync(long id, bool disableTracking = false, params Expression<Func<T, object>>[] propertiesToJoin);
        Task<T?> GetByIdWithOutNestedEntitiesAsync(long id, bool disableTracking = false);
        Task<List<T>> GetAsync(params Expression<Func<T, object>>[] propertiesToJoin);
        Task<T?> DeleteAsync(T entity, bool isForced);
        Task<T> UpdateEntityAsync(T entity);
        Task<T> AddEntityAsync(T entity);
    }
}
