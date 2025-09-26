using CRUD.BLL.IRepositories;
using CRUD.BLL.Services.Abstract.Filter;
using CRUD.BLL.Services.Interfaces.Common;
using System.Linq.Expressions;
namespace CRUD.BLL.Services.Abstract
{
    public abstract class BaseFilterService<T, TFilter, TSort> : IFilterService<T, TFilter, TSort>
        where T : class
        where TFilter : BaseFilterParameters<TSort>
        where TSort : Enum
    {
        protected readonly IRepository<T> _repository;
        public BaseFilterService(IRepository<T> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<int> CountAsync(TFilter parameters) =>
            await CountAsync(parameters, filterPropertiesToJoin);
        private async Task<int> CountAsync(TFilter parameters,
                                                 params Expression<Func<T, object>>[] propertiesToJoin)
        {
            var query = GetFilterQuery(parameters);
            return await _repository.CountAsync(query, propertiesToJoin);
        }

        public async Task<List<T>> FilterAsync(TFilter parameters) =>
             await FilterAsync(parameters, filterPropertiesToJoin);

        private async Task<List<T>> FilterAsync(TFilter parameters,
                                                    params Expression<Func<T, object>>[] propertiesToJoin)
        {
            var query = GetFilterQuery(parameters);
            var sortProperties = GetSortProperties(parameters);
            return await _repository.FilterAsync(query, parameters.Skip, parameters.Take, sortProperties, true, propertiesToJoin);
        }
        protected virtual Expression<Func<T, object>>[] filterPropertiesToJoin =>
           new Expression<Func<T, object>>[] { };


        protected abstract Expression<Func<T, bool>>? GetFilterQuery(TFilter parameters);

        protected List<SortProperty<T>> GetSortProperties(TFilter parameters)
        {
            if (parameters.OrderBy == null
                || !SortPropertiesDictionary().ContainsKey(parameters.OrderBy))
            return new List<SortProperty<T>>();

            return new List<SortProperty<T>>()
                {
                    new SortProperty<T>()
                    {
                        Expression = SortPropertiesDictionary()[parameters.OrderBy],
                        SortDirection = parameters.SortDirection,
                        Order = 1
                    }
                };
        }
        protected virtual Dictionary<TSort, Expression<Func<T, object>>> SortPropertiesDictionary() =>
            new Dictionary<TSort, Expression<Func<T, object>>>();
    }
}