using CRUD.BLL.Helpers;
using CRUD.BLL.IRepositories;
using CRUD.BLL.Models.Foos;
using CRUD.BLL.Services.Abstract;
using CRUD.BLL.Services.Foos.Parameters;
using CRUD.BLL.Services.Interfaces.Foos;
using CRUD.DTO.Enums.Sort;
using System.Linq.Expressions;

namespace CRUD.BLL.Services.Foos
{
    public class FooService(IRepository<Foo> _repository)
                : BaseFilterService<Foo, FooFilterParameters, FooSort>(_repository), IFooService
    {
        public async Task<bool> CanBeForcedDeletedAsync(long id)
        {
            return true;
        }

        protected override Expression<Func<Foo, object>>[] filterPropertiesToJoin =>
                new Expression<Func<Foo, object>>[] { x => x.FooFoo };

        protected override Expression<Func<Foo, bool>> GetFilterQuery(FooFilterParameters parameters)
        {
            Expression<Func<Foo, bool>> query = e => true;

            if (!String.IsNullOrEmpty(parameters.Title))
                query = query.And(e => e.Title.ToLower().Contains(parameters.Title.ToLower().Trim()));

            query = query.And(e => e.IsDeleted == parameters.IsDeleted);

            return query;
        }
        protected override Dictionary<FooSort, Expression<Func<Foo, object>>> SortPropertiesDictionary()
            => _propertySortDictionary;

        private static readonly Dictionary<FooSort, Expression<Func<Foo, object>>>
            _propertySortDictionary =
            new Dictionary<FooSort, Expression<Func<Foo, object>>>()
            {
                { FooSort.Title, x=> x.Title},
                { FooSort.FooFoo, x=> x.FooFoo.Value},
            };
    }
}
