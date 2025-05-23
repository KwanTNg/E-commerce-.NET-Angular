using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// StoreContext is the type
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//Scope to the lifetime of HTTP request
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Anything before this line is considered service
// Anything after this line is considered middleware
var app = builder.Build();

app.MapControllers();

app.Run();
