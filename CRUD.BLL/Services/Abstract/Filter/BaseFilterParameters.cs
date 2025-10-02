using CRUD.DTO.Enums.Sort;

namespace CRUD.BLL.Services.Abstract.Filter
{
    public class BaseFilterParameters<T>
        where T : Enum
    {
        public virtual int? Skip { get; set; } = 0;
        public virtual int? Take { get; set; } = 20;
        public bool IsDeleted { get; set; } = false;
        public virtual T? OrderBy { get; set; }
        public virtual SortDirection SortDirection { get; set; } = SortDirection.Asc;

    }
}
