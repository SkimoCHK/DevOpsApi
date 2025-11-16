using ApartadoAulasAPI.Exceptions;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApartadoAulasAPI.Middlewares
{
  public class ExceptionHandlingMiddleware
  {
    private readonly RequestDelegate _next;
    public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        await HandleExceptionAsync(context, ex);
      }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = 500;

      dynamic responseException = new ExpandoObject();
      responseException.Message = "Error en el servidor";
      responseException.StatusCode = 500;


      if (ex is HttpException exception)
      {
        context.Response.StatusCode = exception.StatusCode;
        responseException.StatusCode = exception.StatusCode;
        responseException.Message = exception.Message;
      }
      else
      {
        responseException.InternalExceptionMessage = ex.Message;
        responseException.ClassNameException = ex.GetType().FullName;
        responseException.MethodNameException = ex.TargetSite?.Name ?? "Método desconocido";
        responseException.Data = ex.Data;
      }
      return context.Response.WriteAsync(JsonSerializer.Serialize(responseException as ExpandoObject));
    }
  }
}
