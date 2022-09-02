namespace dotnet_docker_webapi.Interfaces
{
    public interface IResponse
    {
        bool Success { get; }
        int StatusCode { get; }
        string? Message { get; }
    }
}