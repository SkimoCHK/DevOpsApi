
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.PostgreConfiguration;
using ApartadoAulasAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace ApartadoAulasAPI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      var connectionString = builder.Configuration.GetConnectionString("PostgreConnection");

      builder.Services.AddDbContext<AppDbContext>(o =>
      {
        o.UseNpgsql(connectionString);
      });

      builder.Services.AddScoped<IProductService, ProductService>();

      builder.Services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }
  }
}
