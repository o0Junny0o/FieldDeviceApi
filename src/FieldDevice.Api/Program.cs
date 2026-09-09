using FieldDevice.Api.Data;
using FieldDevice.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var conn = builder.Configuration.GetConnectionString("DefaultConnection");

// If no connection string, use in-memory DB for quick start
if (string.IsNullOrEmpty(conn))
{
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("FieldDeviceDb"));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(conn));
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Global error handling
app.UseExceptionHandler("/error");

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/error", () => Results.Problem("An unexpected error occurred."));
app.MapControllers();

app.Run();
