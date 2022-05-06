namespace dotnet_docker_webapi.Models
{
    public class Response : IResponse
    {
        public bool Success { get; }
        public int StatusCode { get; }
        public string Message { get; set; } = ""!;

        public Response(int statusCode, bool success)
        {
            StatusCode = statusCode;
            Success = success;
        }

        public Response(int statusCode, bool success, string message)
        {
            StatusCode = statusCode;
            Success = success;
            Message = message;
        }
    }
}