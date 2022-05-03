using System.ComponentModel.DataAnnotations;

namespace dotnet_docker_webapi.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}
