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
//Because we don't know the type, so use typeof
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Anything before this line is considered service
// Anything after this line is considered middleware
var app = builder.Build();

app.MapControllers();

//this is use outside of dependency injection
try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    //if database does not exist, automatically create a database
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}

app.Run();
