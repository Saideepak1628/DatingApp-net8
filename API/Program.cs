using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200", "https://localhost:4200") // Allow Angular app
                  .AllowAnyMethod() // Allow all HTTP methods (GET, POST, PUT, DELETE, etc.)
                  .AllowAnyHeader() // Allow all headers
                  .AllowCredentials(); // Allow cookies/auth headers if needed
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// connecting our entityframework to our sqlite
builder.Services.AddDbContext<DataContext>(opt => 
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else {
app.UseHttpsRedirection();
}
// Use CORS before other middlewares
app.UseCors("AllowAngularApp");
app.UseAuthorization();


app.MapControllers(); // it maps the controller 

app.Run(); // it runs our application
