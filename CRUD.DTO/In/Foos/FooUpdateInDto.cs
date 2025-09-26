using CRUD.DTO.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CRUD.DTO.In.Foos
{
    /// <summary>
    /// Обновление Foo
    /// </summary>
    public class FooUpdateInDto
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
