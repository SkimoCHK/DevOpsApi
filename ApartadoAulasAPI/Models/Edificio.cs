using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartadoAulasAPI.Models
{
  public class Edificio
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; }

    [Required]
    public bool Estatus { get; set; }

    [ForeignKey("Encargado")]
    public int EncargadoId { get; set; }

    public Usuario Encargado { get; set; }
    
  }
}
