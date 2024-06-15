using Application.Services;
using Core.Interfaces;
using Infrastructure.Data;
using Npgsql;
using System.Data;
using Microsoft.Extensions.Logging;
using WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configura el logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddEventSourceLogger();

// Add services to the container.
builder.Services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Use the exception middleware
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
