using CRUD.DTO.Enums.Sort;
using System.Linq.Expressions;

namespace CRUD.BLL.Services.Abstract.Filter
{
    /// <summary>
    /// Параметры сортировки
    /// </summary>
    public class SortProperty<T>
    {
        /// <summary>
        /// Имя свойства для сортировки
        /// </summary>
        public Expression<Func<T, object>> Expression { get; set; }
        /// <summary>
        /// Сортировать по убыванию
        /// </summary>
        public SortDirection SortDirection { get; set; } = SortDirection.Asc;
        /// <summary>
        /// Порядок сортировки
        /// </summary>
        public int? Order { get; set; } = 1;
    }

}
