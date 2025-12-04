using ApartadoAulasAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartadoAulasAPI.DTOs.Aula
{
  public class CreateAulaDto
  {
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    [Required]
    public int CapacidadEstudiantes { get; set; }
    [Required]
    public bool Estatus { get; set; }
    [Required]
    public int TipoAulaId { get; set; }
    [Required]
    public int EdificioId { get; set; }
  }
}
