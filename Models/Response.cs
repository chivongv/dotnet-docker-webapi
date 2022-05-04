namespace dotnet_docker_webapi.Models
{
    public class Response : IResponse
    {
        public bool Success { get; }
        public int StatusCode { get; }

        public Response(int statusCode, bool success)
        {
            StatusCode = statusCode;
            Success = success;
        }
    }
}