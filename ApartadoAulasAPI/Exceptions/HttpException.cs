namespace ApartadoAulasAPI.Exceptions
{
  public class HttpException : Exception
  {
    public int StatusCode { get; private set; }
    public string ErrorMessage { get; private set; }
    public HttpException(int statusCode, string errorMessage) : base(errorMessage)
    {
      StatusCode = statusCode;
      ErrorMessage = errorMessage;
    }
    public HttpException(int statusCode) : base($"Error: código {statusCode}")
    {
      StatusCode = statusCode;
      ErrorMessage = $"Error: código {StatusCode}";
    }
    public HttpException() : base($"Error en el servidor: inténtalo más tarde")
    {
      StatusCode = 500;
      ErrorMessage = "Error en el servidor: inténtalo más tarde";
    }



  }
}
