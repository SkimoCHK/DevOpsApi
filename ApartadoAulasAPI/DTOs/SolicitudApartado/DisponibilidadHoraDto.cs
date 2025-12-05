namespace ApartadoAulasAPI.DTOs.SolicitudApartado
{
  public class DisponibilidadHoraDto
  {
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public bool Disponible { get; set; }
  }
}
