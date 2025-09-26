namespace CRUD.BLL.Managers.Abstract.Responses
{
    public class FilterManagerResponse<T>
    {
        public IEnumerable<T> Items { get; set; }
        virtual public int TotalCount { get; set; }
    }
}
