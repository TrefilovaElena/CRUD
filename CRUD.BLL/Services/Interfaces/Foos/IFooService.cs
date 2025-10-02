using CRUD.BLL.Models.Foos;
using CRUD.BLL.Services.Interfaces.Common;
using CRUD.BLL.Services.Foos.Parameters;
using CRUD.DTO.Enums.Sort;

namespace CRUD.BLL.Services.Interfaces.Foos
{
    public interface IFooService
                : IFilterService<Foo, FooFilterParameters, FooSort>

    {
        Task<bool> CanBeForcedDeletedAsync(long id);
    }
}
