using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartadoAulasAPI.Models
{
  public class Roles
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Nombre { get; set; }
    
    [MaxLength(90)]
    public string Descripcion {  get; set; }

    [Required]
    [MinLength(2)]
    public string Clave {  get; set; }

    [Required]
    public bool Estatus {  get; set; }
  }
}
