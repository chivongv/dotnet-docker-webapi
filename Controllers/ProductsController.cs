using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Npgsql;
using dotnet_docker_webapi.Models;
using dotnet_docker_webapi.Contexts;

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
    public async Task<IResponse> GetAll()
    {
        var products = await _context.Products.ToListAsync();

        return new DataResponse<List<Product>>(200, products);
    }

    [HttpGet("{id}")]
    public async Task<IResponse> GetById(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product is null)
        {
            return new Response(404, false);
        }

        return new DataResponse<Product>(200, product);
    }

    [HttpPost]
    public async Task<IResponse> AddProduct(Product obj)
    {
        _context.Products.Add(obj);
        await _context.SaveChangesAsync();

        return new Response(200, true);
    }

    [HttpPut]
    public async Task<IResponse> UpdateProduct(Product obj)
    {
        try
        {
            _context.Products.Update(obj);
            await _context.SaveChangesAsync();

            return new Response(200, true);
        }
        catch (Exception e)
        {
            return new Response(400, false);
        }

    }

    [HttpDelete("{id}")]
    public async Task<IResponse> RemoveProductById(int? id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product is not null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return new Response(200, true);
        }

        return new Response(404, false);
    }
}