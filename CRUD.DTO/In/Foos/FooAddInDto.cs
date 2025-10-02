using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CRUD.DTO.In.Foos
{
    /// <summary>
    /// Добавление Foo
    /// </summary>
    public class FooAddInDto
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        [Required]
        [JsonRequired]
        public string Title { get; set; }
        /// <summary>
        /// FooFooId
        /// </summary>
        [Required]
        [JsonRequired]
        public long FooFooId { get; set; }
    }
}
