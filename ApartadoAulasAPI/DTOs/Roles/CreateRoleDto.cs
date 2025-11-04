using System.ComponentModel.DataAnnotations;

namespace ApartadoAulasAPI.DTOs.Roles
{
  public class CreateRoleDto
  {
   
    public string Nombre { get; set; }

    [MaxLength(90)]
    public string Descripcion { get; set; }

    public string Clave { get; set; }

    [Required]
    public bool Estatus { get; set; }


  }
}
