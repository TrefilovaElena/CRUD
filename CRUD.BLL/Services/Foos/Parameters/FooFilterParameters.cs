using CRUD.BLL.Services.Abstract.Filter;
using CRUD.DTO.Enums.Sort;

namespace CRUD.BLL.Services.Foos.Parameters
{
    public class FooFilterParameters : BaseFilterParameters<FooSort>
    {
        public string? Title { get; set; }
        public long FooFooId { get; set; }
    }
}
