namespace PostApi.Models
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public required string Message { get; set; }
    }
}