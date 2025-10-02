using CRUD.DTO.Enums.Sort;
namespace CRUD.BLL.Managers.Abstract.Requests.Filter
{
    public class BaseFilterManagerRequest<T>
        where T : Enum
    {
        public int? Skip { get; set; } = 0;
        public int? Take { get; set; } = 20;
        public bool IsDeleted { get; set; } = false;
        public T? OrderBy { get; set; }
        public bool? DescendingSortDirection { get; set; } 
        public SortDirection SortDirection => (DescendingSortDirection ?? false) 
                          ? SortDirection.Asc 
                          : SortDirection.Desc;
    }
}
