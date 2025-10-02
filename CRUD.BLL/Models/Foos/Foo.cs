using CRUD.DAL.Repositories.Abstract;

namespace CRUD.BLL.Models.Foos
{
    public class Foo : IEntity, ICanBeSoftDeletedModel
    {
        public long Id { get; set; }
        public required string Title { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; } = DateTimeOffset.UtcNow;
        public bool IsDeleted { get; set; } = false;

        public long FooFooId { get; set; }
        public virtual FooFoo? FooFoo { get; set; }
    }
}
