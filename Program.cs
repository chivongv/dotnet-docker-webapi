using Microsoft.EntityFrameworkCore;
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
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductContext>(options =>
{
    options.UseNpgsql(connection);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
