namespace CRUD.BLL.Managers.Abstract.Requests
{
    /// <summary>
    /// Удаление
    /// </summary>
    public class DeleteManagerRequest
    {
        /// <summary>
        /// Полностью удалить: доступно только, если ничто не ссылается 
        /// </summary>
        public bool IsForced { get; set; }
    }
}
