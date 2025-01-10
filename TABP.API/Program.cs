using Microsoft.EntityFrameworkCore;
using TABP.Application.ApplicationServices;
using TABP.Infrastructure.InfrastructureServices;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure DbContext with SQL Server (or your database provider)
builder.Services.AddDbContext<InfrastructureDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add other services (Swagger, API Explorer, etc.)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

builder.Services.AddApplicationServices().AddInfrastructureServices();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
