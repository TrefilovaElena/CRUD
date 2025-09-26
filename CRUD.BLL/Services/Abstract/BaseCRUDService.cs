using CRUD.BLL.IRepositories;
using CRUD.BLL.Models;
using CRUD.BLL.Services.Abstract.Filter;
using CRUD.BLL.Services.Interfaces.Common;
using CRUD.DAL.Repositories.Abstract;
using CRUD.DTO.Enums.Sort;
using System.Linq.Expressions;
namespace CRUD.BLL.Services.Abstract
{
    public class BaseCRUDService<T> : ICRUDService<T>
        where T : class, ICanBeSoftDeletedModel, IEntity, new()
    {
        protected readonly IRepository<T> _repository;
        public BaseCRUDService(IRepository<T> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<List<T>> GetAsync(params Expression<Func<T, object>>[] propertiesToJoin)
        {
            Expression<Func<T, bool>>? actualOnly = e => !e.IsDeleted;
            IEnumerable<SortProperty<T>>? sortByIdDesc = new List<SortProperty<T>> ()
            { new SortProperty<T>()
                    {
                        Expression = x=> x.Id,
                        SortDirection =  SortDirection.Desc,
                        Order = 1
                    } };
            return await _repository.FilterAsync(actualOnly, null, null, sortByIdDesc, true, propertiesToJoin);
        }

        public virtual async Task<T?> GetByIdWithOutNestedEntitiesAsync(long id, bool disableTracking = false)
        {
            return await _repository.GetByIdAsync(id, disableTracking);
        }

        public async Task<T?> GetByIdAsync(long id, bool disableTracking = true, params Expression<Func<T, object>>[] propertiesToJoin)
        {
            return await _repository.GetByIdAsync(id, disableTracking, propertiesToJoin);
        }

        public async Task<T?> DeleteAsync(T model, bool isForced)
        {
            if (isForced)
            {
                await _repository.RemoveAsync(model);
                return null;
            }
            else
            {
                model.IsDeleted = true;
                await _repository.UpdateAsync(model);
                return model;
            }
        }

        public async Task<T> UpdateEntityAsync(T model)
        {
            return await _repository.UpdateAsync(model);
        }

        public async Task<T> AddEntityAsync(T model)
        {
            return await _repository.CreateAsync(model);
        }
    }
}