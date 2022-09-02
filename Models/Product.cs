using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using dotnet_docker_webapi.Interfaces;

namespace dotnet_docker_webapi.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = default!;
}
