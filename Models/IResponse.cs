namespace dotnet_docker_webapi.Models
{
    public interface IResponse
    {
        bool Success { get; }
        int StatusCode { get; }
        string? Message { get; }
    }
}