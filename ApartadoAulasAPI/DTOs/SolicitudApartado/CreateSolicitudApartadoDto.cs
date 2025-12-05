using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartadoAulasAPI.DTOs.SolicitudApartado
{
  public class CreateSolicitudApartadoDto
  {
    [Required]
    public DateOnly Fecha { get; set; }
    [Required]
    public TimeOnly HoraInicio { get; set; }
    [Required]
    public TimeOnly HoraFin { get; set; }
    [Required]
    public string Motivo { get; set; }   
    [Required]
    public int UsuarioId { get; set; }
    [Required]
    public int AulaId { get; set; }
  }
}
