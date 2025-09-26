using CRUD.BLL.Models.Foos;

namespace CRUD.BLL.Managers.Foos.Responses
{
    public class FooManagerResponse
    {
        public long Id { get; set; }
        public required string Title { get; set; }
        public required long FooFooId { get; set; }
        public long? FooFooValue { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
    }
}
