var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Anything before this line is considered service
// Anything after this line is considered middleware
var app = builder.Build();

app.MapControllers();

app.Run();
