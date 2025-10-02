using CRUD.BLL.Managers.Abstract.Requests;
using CRUD.BLL.Managers.Abstract.Responses;
using CRUD.BLL.Managers.Foos.Requests.Filter;
using CRUD.BLL.Managers.Foos.Responses;

namespace CRUD.BLL.Managers.Interfaces.Foos
{
    public interface IFooManager
    {
        Task<ManagerResult<FilterManagerResponse<FooManagerResponse>>> FilterAsync(FooFilterManagerRequest request);
    }
}