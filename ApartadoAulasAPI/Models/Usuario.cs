using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartadoAulasAPI.Models
{
  public class Usuario
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    public string Password { get; set; }

    [Required]
    public bool Estatus {  get; set; }

    [Required]
    public DateTime FechaRegistro { get; set; }

    public int RolId { get; set; }

    public Roles Rol { get; set; }

  }
}
