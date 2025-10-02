using System.ComponentModel.DataAnnotations;

namespace CRUD.BLL.Managers.Foos.Requests.Add
{
    public class FooAddManagerRequest
    {
        public string Title { get; set; }
        public long FooFooId { get; set; }
    }
}
