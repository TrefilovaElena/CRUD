using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CRUD
    .DTO.Abstract.Filter
{
    public abstract class BaseFilterInDto<T>
         where T : Enum
    {
        /// <summary>
        /// Номер страницы
        /// </summary>
        [Range(0, int.MaxValue)]
        [JsonProperty(PropertyName = "skip", Order = 1)]
        public virtual int? Skip { get; set; } = 0;
        /// <summary>
        /// Размер страницы
        /// </summary>
        [Range(1, 1000)]
        [JsonProperty(PropertyName = "take", Order = 2)]
        public virtual int? Take { get; set; } = 20;
        /// <summary>
        /// Сортировка по полю
        /// </summary>
        [JsonProperty(PropertyName = "orderBy", Order = 4)]
        public virtual T? OrderBy { get; set; }
        /// <summary>
        /// true, если нужно отсортировать descending
        /// </summary>
        [JsonProperty(PropertyName = "descendingSortDirection", Order = 5)]
        public virtual bool? DescendingSortDirection { get; set; } = true;
    }
}
