using CRUD.BLL.Managers.Abstract.Requests.Filter;
using CRUD.DTO.Enums;
using CRUD.DTO.Enums.Sort;
using System.ComponentModel.DataAnnotations;

namespace CRUD.BLL.Managers.Foos.Requests.Filter
{
    /// <summary>
    /// Фильтрация Foo
    /// </summary>
    public class FooFilterManagerRequest
        : BaseFilterManagerRequest<FooSort>
    {
        public string? Title { get; set; }
        public long FooFooId { get; set; }
    }
}