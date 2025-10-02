namespace CRUD.DTO.Out.Foos
{
    /// <summary>
    /// Foo без содержания
    /// </summary>
    public class FooTableViewOutDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Заголовок
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// Удалено
        /// </summary>
        public bool IsDeleted { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
    }
}
