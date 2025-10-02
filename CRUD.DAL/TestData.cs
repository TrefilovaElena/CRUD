using CRUD.BLL.Models.Foos;

namespace CRUD.DAL
{
    public static class TestData
    {
        public static readonly FooFoo[] FooFoos = new FooFoo[]
           {
               new FooFoo
                {
                    Id = 1,
                    Value = 1,
                },
                new FooFoo
                {
                    Id = 2,
                    Value = 2
                },         
           };


        public static readonly Foo[] Foos = Enumerable.Range(1, 20)
            .Select(i => new Foo
            {
                Id = i * -1,
                Title = $"Foo {i}",
                FooFooId = i == 20 ? 2 : 1
            }).ToArray();

    }
}