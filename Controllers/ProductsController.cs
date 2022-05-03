using System.Data;
using dotnet_docker_webapi.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace dotnet_docker_webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{

    private readonly IConfiguration _configuration;
    private List<Product> _products = new List<Product> {
        new Product {Id = 1, Name="Apple"},
        new Product {Id = 2, Name="Banana"}
    };

    public ProductsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<List<Product>> GetAll()
    {
        string query = @"
                select Id as ""ProductId"",
                    Name as ""ProductName""
                    from Products
            ";

        List<Product> products = new List<Product>();

        string sqlDataSource = _configuration.GetConnectionString("PostgreSqlConnection");
        await using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
            {
                await using (NpgsqlDataReader myReader = await myCommand.ExecuteReaderAsync())
                {

                    while (await myReader.ReadAsync())
                    {
                        Console.WriteLine("has rows: " + myReader.HasRows);
                        if (myReader.HasRows)
                        {
                            Console.WriteLine("rows: " + myReader.Rows);
                            Console.WriteLine("myReader: " + myReader);
                            try
                            {
                                int? id = myReader["id"] as int?;
                                string name = myReader["name"] as string;

                                if (id > 0 && name != "")
                                {
                                    Product product = new Product { Id = id.Value, Name = name };
                                    products.Add(product);
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Soemthing went wrong " + e);
                            }
                        }
                    }

                    myReader.Close();
                    myCon.Close();
                }
            }
        }

        return products;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        string query = @"
                select Id as ""ProductId"",
                    Name as ""ProductName""
                    from Products where Id=@ProductId
            ";

        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("PostgreSqlConnection");
        NpgsqlDataReader myReader;
        using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@ProductId", id);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);

                myReader.Close();
                myCon.Close();
            }
        }

        Console.WriteLine(table);

        return Ok(table);
    }
}