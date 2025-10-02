using CRUD.BLL.Services.Abstract.Filter;

namespace CRUD.BLL.Services.Interfaces.Common
{
    public interface IFilterService<T, TFilter, TSort>
        where T : class
        where TFilter : BaseFilterParameters<TSort>
        where TSort : Enum
    {
        Task<List<T>> FilterAsync(TFilter parameters);
        Task<int> CountAsync(TFilter parameters);
    }
}
