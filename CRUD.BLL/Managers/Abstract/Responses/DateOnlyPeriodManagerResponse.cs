namespace CRUD.BLL.Managers.Abstract.Responses
{
    /// <summary>
    /// Период дат
    /// </summary>
    public class DateOnlyPeriodManagerResponse
    {
        /// <summary>
        /// Начальная дата, когда занято
        /// </summary>
        public DateOnly DateFrom { get; set; }
        /// <summary>
        /// Конечная дата, когда занято
        /// </summary>
        public DateOnly DateTo { get; set; }
    }

}
