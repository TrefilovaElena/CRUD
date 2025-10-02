namespace CRUD.BLL.Managers.Abstract.Requests
{
    public class ManagerResult<T>
    {
        public bool IsSuccess { get; set; }
        public ManagerStatus Status { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Object { get; set; }

        public static ManagerResult<T> CreateSuccessResult(ManagerStatus status, T result)
        {
            return new ManagerResult<T>
            {
                Status = status,
                Object = result,
                IsSuccess = true
            };
        }
        public static ManagerResult<T> CreateErrorResult(ManagerStatus status, string errorMessage)
        {
            return new ManagerResult<T>
            {
                Status = status,
                ErrorMessage = errorMessage,
                IsSuccess = false
            };
        }
    }

    public enum ManagerStatus
    {
        BadRequest = 400,
        Created = 201,
        NotFound = 404,
        Forbidden = 403,
        Ok = 200,
    }
}
