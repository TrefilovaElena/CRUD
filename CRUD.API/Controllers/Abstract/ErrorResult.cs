namespace CRUD.API.Controllers.Abstract
{
    public class ErrorResult
    {
        public string? ErrorMessage { get; set; } = string.Empty;
        public int Status { get; set; }
        public ErrorResult(string? errorMessage, int status)
        {
            ErrorMessage = errorMessage;
            Status = status;
        }
    }
}
