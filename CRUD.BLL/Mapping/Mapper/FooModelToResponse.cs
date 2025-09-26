using CRUD.BLL.Managers.Foos.Responses;
using CRUD.BLL.Models.Foos;

namespace CRUD.BLL.Mapping.Mapper
{
    public static partial class FooMapper
    {
        public static FooManagerResponse? GetFoosResponse(Foo foo)
        {
            if (foo == null)
                return null;

            return new FooManagerResponse
            {
                Id = foo.Id,
                IsDeleted = foo.IsDeleted,
                Title = foo.Title,
                LastModifiedDate = foo.LastModifiedDate,
                FooFooId = foo.FooFooId,
                FooFooValue = foo.FooFoo?.Value,
            };
        }
 
    }
}