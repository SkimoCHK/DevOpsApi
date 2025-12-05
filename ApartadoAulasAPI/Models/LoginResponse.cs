namespace ApartadoAulasAPI.Models
{
  public class LoginResponse
  {
    public int IdUsuario { get; set; }
    public string Nombre { get; set; }
    public int TotalReservas { get; set; }
    public int TotalActivasHoy { get; set; }
    public List<SolicitudApartado> ProximasReservas { get; set; }
  }
}
