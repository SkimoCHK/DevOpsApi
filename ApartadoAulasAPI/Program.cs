using ApartadoAulasAPI.AutoMappers;
using ApartadoAulasAPI.DTOs.Aula;
using ApartadoAulasAPI.DTOs.Edificio;
using ApartadoAulasAPI.DTOs.Roles;
using ApartadoAulasAPI.DTOs.Usuario;
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Middlewares;
using ApartadoAulasAPI.Models;
using ApartadoAulasAPI.PostgreConfiguration;
using ApartadoAulasAPI.Repositories;
using ApartadoAulasAPI.Services;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApartadoAulasAPI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // NUEVO: Configurar para escuchar en el puerto 8080
      builder.WebHost.ConfigureKestrel(serverOptions =>
      {
        serverOptions.ListenAnyIP(8080);
      });

      // NUEVO: Agregar variables de entorno
      builder.Configuration.AddEnvironmentVariables();

      // Add services to the container.
      // MODIFICADO: Usar DefaultConnection en lugar de PostgreConnection
      var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                             ?? builder.Configuration.GetConnectionString("PostgreConnection");

      builder.Services.AddDbContext<AppDbContext>(o =>
      {
        o.UseNpgsql(connectionString);
      });

      //Validators
      builder.Services.AddScoped<IRepository<Roles>, RolesRepository>();
      builder.Services.AddScoped<IRepository<Usuario>, UserRepository>();
      builder.Services.AddScoped<IRepository<Edificio>, EdificioRepository>();
      builder.Services.AddScoped<IRepository<Aula>, AulaRepository>();
      builder.Services.AddScoped<ICommonService<Roles, CreateRoleDto, UpdateRoleDto>, RolesService>();
      builder.Services.AddScoped<ICommonService<Usuario, CreateUserDto, UpdateUserDto>, UsuarioService>();
      builder.Services.AddScoped<ICommonService<Edificio, CreateEdificioDto, UpdateEdificioDto>, EdificioService>();
      builder.Services.AddScoped<ICommonService<Aula, CreateAulaDto, UpdateAulaDto>, AulaService>();
      builder.Services.AddScoped<AulaRepository>();
      builder.Services.AddScoped<SolicitudApartadoRepository>();
      builder.Services.AddScoped<SolicitudApartadoService>();
      builder.Services.AddScoped<AuthService>();
      builder.Services.AddControllers();

      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      //Configurar CORS (acepta cualquier origen)
      builder.Services.AddCors(options =>
      {
        options.AddPolicy("PermitirTodo", policy =>
        {
          policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
      });

      // Register AutoMapper before building the app
      builder.Services.AddAutoMapper(typeof(Program).Assembly);

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      // MODIFICADO: Mostrar Swagger también en producción (opcional)
      app.UseSwagger();
      app.UseSwaggerUI();

      app.UseMiddleware<ExceptionHandlingMiddleware>();

      //Test middleware in program
      app.Use(async (context, next) =>
      {
        Console.WriteLine("Incio del M2");
        await next(context);
        Console.WriteLine("Fin del M2");
      });

      // MODIFICADO: Comentar HTTPS redirect para Render (ellos manejan SSL)
      // app.UseHttpsRedirection();

      app.UseCors("PermitirTodo");
      app.UseAuthorization();
      app.MapControllers();
      app.Run();
    }
  }
}