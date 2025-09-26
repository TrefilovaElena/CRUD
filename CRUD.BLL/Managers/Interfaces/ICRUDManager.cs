using CRUD.BLL.Managers.Abstract.Requests;

namespace CRUD.BLL.Managers.Interfaces
{
    /// <summary>
    /// Подготовка данных для выполнения задач сервисом
    /// </summary>
    public interface ICRUDManager<T, RequestADD, RequestUpdate, Response>
        where T : class
    {
        Task<ManagerResult<Response?>> GetByIdAsync(long id);
        Task<ManagerResult<List<Response>>> GetAsync();
        Task<ManagerResult<Response?>> UpdateAsync(RequestUpdate dto, long id);
        Task<ManagerResult<Response?>> AddAsync(RequestADD dto);
        Task<ManagerResult<bool>> DeleteAsync(long id, DeleteManagerRequest dto);
    }
}
