namespace ApartadoAulasAPI.Exceptions
{
  public class HttpsException : Exception
  {
    public int ErrorCode { get; set; }


    public HttpsException(int errorCode, string errorMessage) : base(errorMessage)
    {
      ErrorCode = errorCode;
    }
    public HttpsException(int errorCode) : base($"Error: codigo {errorCode}"){
      ErrorCode = errorCode;
    }

    public HttpsException() : this(500, "Error en el servidor") { }


  
  }
}
