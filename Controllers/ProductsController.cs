using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using dotnet_docker_webapi.Models;
using dotnet_docker_webapi.Contexts;
using dotnet_docker_webapi.DTO;

namespace dotnet_docker_webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{

    private readonly ProductContext _context;

    public ProductsController(ProductContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<Product>> GetAll()
    {
        var products = await _context.Products.ToListAsync();

        return products;
    }

    [HttpGet("{id}")]
    public async Task<Product> GetById(int id)
    {
        var product = await _context.Products.FindAsync(id);

        return product;
    }

}