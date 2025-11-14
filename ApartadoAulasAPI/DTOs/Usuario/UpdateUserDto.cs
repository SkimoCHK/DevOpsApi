using System.ComponentModel.DataAnnotations;

namespace ApartadoAulasAPI.DTOs.Usuario
{
  public class UpdateUserDto
  {
    [Required]
    public int Id { get; set; }

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
    [MinLength(8)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).+$",
    ErrorMessage = "El password debe tener mayúsculas, minúsculas y números.")]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
    public string ConfirmPassword { get; set; }


    [Required]
    public bool Estatus { get; set; }

    [Required]
    public int RolId { get; set; }
  }
}
