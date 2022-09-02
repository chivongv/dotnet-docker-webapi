namespace dotnet_docker_webapi.Interfaces
{
    public interface IDataResponse<T> : IResponse
    {
        T Data { get; }
    }
}