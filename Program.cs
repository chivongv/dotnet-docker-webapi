using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using dotnet_docker_webapi.Contexts;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("PostgreSqlConnection") + "Password=" + builder.Configuration["DbPassword"] + ";";

// Add services to the container.

builder.Services.AddControllers();
// .AddNewtonsoftJson(opts =>
// {
//     opts.SerializerSettings.Converters.Add(new StringEnumConverter());
//     opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
// })
//    .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
  {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "Products API", Description = "Simple products", Version = "v1" });
  });
builder.Services.AddDbContext<ProductContext>(options =>
{
    options.UseNpgsql(connection);
});
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    // builder.WithOrigins("http://example.com","*");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
// app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();

app.Run();
