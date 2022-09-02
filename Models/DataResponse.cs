using dotnet_docker_webapi.Interfaces;

namespace dotnet_docker_webapi.Models
{
    public class DataResponse<T> : IDataResponse<T>
    {
        public bool Success { get; } = true;
        public T Data { get; }
        public int StatusCode { get; }
        public string Message { get; set; } = ""!;

        public DataResponse(int statusCode, T data)
        {
            StatusCode = statusCode;
            Data = data;
        }

        public DataResponse(int statusCode, T data, string message)
        {
            StatusCode = statusCode;
            Data = data;
            Message = message;
        }
    }
}