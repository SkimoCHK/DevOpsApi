using ApartadoAulasAPI.DTOs.Roles;
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using ApartadoAulasAPI.PostgreConfiguration;
using ApartadoAulasAPI.Repositories;
using ApartadoAulasAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using ApartadoAulasAPI.AutoMappers;
using FluentValidation;
using FluentValidation.AspNetCore;

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

            //Validators



            builder.Services.AddScoped<IRepository<Roles>, RolesRepository>();
            builder.Services.AddScoped<ICommonService<Roles, CreateRoleDto, UpdateRoleDto>, RolesService>();

            //builder.Services.AddControllers();

            builder.Services.AddControllers()
              .AddFluentValidation();

            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

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
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("PermitirTodo");
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
