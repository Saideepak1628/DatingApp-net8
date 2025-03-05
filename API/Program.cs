
using API.ExceptionMiddleware;
using API.Extensions;


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

app.Run(); // it runs our application
