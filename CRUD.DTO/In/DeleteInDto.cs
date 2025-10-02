namespace CRUD.DTO.In
{
    /// <summary>
    /// Удаление
    /// </summary>
    public class DeleteInDto
    {
        /// <summary>
        /// Полностью удалить: доступно только, если ничто не ссылается 
        /// </summary>
        public bool IsForced { get; set; }
    }
}
