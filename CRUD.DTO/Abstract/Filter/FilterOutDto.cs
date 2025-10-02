using Newtonsoft.Json;

namespace CRUD.DTO.Abstract.Filter
{
    public class FilterOutDto<T>
    {
        /// <summary>
        /// Элементы
        /// </summary>
        [JsonProperty(Order = 1, PropertyName = "items")]
        public List<T> Items { get; set; }

        /// <summary>
        /// Всего элементов
        /// </summary>
        [JsonProperty(Order = 4, PropertyName = "totalCount")]
        virtual public int TotalCount { get; set; }
    }
}
