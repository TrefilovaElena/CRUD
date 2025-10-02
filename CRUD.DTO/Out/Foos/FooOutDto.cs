using CRUD.DTO.Enums;

namespace CRUD.DTO.Out.Foos
{
    /// <summary>
    /// Foo с содержанием
    /// </summary>
    public class FooOutDto : FooTableViewOutDto
    {
        /// <summary>
        /// FooFooValue
        /// </summary>
        public long FooFooValue { get; set; }
    }
}
