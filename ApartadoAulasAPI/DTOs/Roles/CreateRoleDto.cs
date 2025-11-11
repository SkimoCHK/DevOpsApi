using System.ComponentModel.DataAnnotations;

namespace ApartadoAulasAPI.DTOs.Roles
{
  public class CreateRoleDto
  {

    [Required]
    [MaxLength(35)]
    public string Nombre { get; set; }

    [MaxLength(90)]
    public string Descripcion { get; set; }

    [Required]
    [MinLength(2)]
    public string Clave { get; set; }

    [Required]
    public bool Estatus { get; set; }


  }
}
