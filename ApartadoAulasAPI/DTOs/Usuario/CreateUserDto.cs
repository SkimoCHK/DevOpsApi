using System.ComponentModel.DataAnnotations;

namespace ApartadoAulasAPI.DTOs.Usuario
{
  public class CreateUserDto
  {

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; }

    [Required]
    [MaxLength(100)]
    public string Apellido { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }


    [Required]
    public bool Estatus { get; set; }

    [Required]
    public DateTime FechaRegistro {
      get 
      { 
        return FechaRegistro; 
      } 
      set
      {
        if(value > DateTime.Now)
        {
          value = DateTime.Now;
        }
        this.FechaRegistro = value;
      } 
    
    }

    [Required]
    public int RolId { get; set; }

  }
}
