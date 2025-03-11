
using API.Data;
using API.ExceptionMiddleware;
using API.Extensions;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add CORS policy
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentityServives(builder.Configuration);

var app = builder.Build();

// middleware
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else 
{
app.UseHttpsRedirection();
}


// Use CORS before other middlewares
app.UseCors("AllowAngularApp");


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers(); // it maps the controller 

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try 
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}

catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during the migration");
}
app.Run(); // it runs our application
