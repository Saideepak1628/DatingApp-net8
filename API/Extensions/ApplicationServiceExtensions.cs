using API.Data;
using API.Interface;
using API.Service;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
  public static class ApplicationServiceExtensions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200", "https://localhost:4200") 
                  .AllowAnyMethod() 
                  .AllowAnyHeader() 
                  .AllowCredentials(); 
        });
});

      services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();

      // connecting our entityframework to our sqlite
      services.AddDbContext<DataContext>(opt =>
      {
        opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
      });


      services.AddScoped<ITokenService, TokenService>();

      services.AddScoped<IUserRepository, UserRepository>();

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      return services;
    }
  }
}