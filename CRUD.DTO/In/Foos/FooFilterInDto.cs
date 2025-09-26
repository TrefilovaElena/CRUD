using CRUD.DTO.Abstract.Filter;
using CRUD.DTO.Enums.Sort;
using System.ComponentModel.DataAnnotations;

namespace CRUD.DTO.In.Foos
{
    /// <summary>
    /// Фильтрация Foo
    /// </summary>
    public class FooFilterInDto : BaseFilterInDto<FooSort>
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// FooFooId
        /// </summary>
        public long? FooFooId { get; set; }

    }
}
