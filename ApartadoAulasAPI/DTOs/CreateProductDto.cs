
using System.ComponentModel.DataAnnotations;

namespace ApartadoAulasAPI.DTOs
{
  public class CreateProductDto
  {
    [Required]
    [MinLength(3, ErrorMessage = "Minimo deben ser 3 caracteres")]
    public string Name { get; set; }
    public decimal Price { get; set; }
  }
}
