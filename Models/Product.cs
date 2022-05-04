using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace dotnet_docker_webapi.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}
