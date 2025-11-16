using ApartadoAulasAPI.Models;
using System.Text.Json;

namespace ApartadoAulasAPI.Middlewares
{
  public class MiddlewareExceptionHandler
  {
    private readonly RequestDelegate _next;
    public MiddlewareExceptionHandler(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
      Console.WriteLine("Antes del M1");
      Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

      //Pasamos la solicitud al sig middleware
      await _next(context);

      Console.WriteLine("Fin del M1");
      Console.WriteLine($"Response: {context.Response.StatusCode}");
     
    }


  }
}
