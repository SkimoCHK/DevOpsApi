using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartadoAulasAPI.DTOs.Edificio
{
  public class CreateEdificioDto
  {
    [Required]
    [MaxLength(100, ErrorMessage = "El máximo de caracteres permitidos es de 100")]
    public string Nombre { get; set; }

    [Required]
    public bool Estatus { get; set; }

    [ForeignKey("Encargado")]
    public int EncargadoId { get; set; }
  }
}
