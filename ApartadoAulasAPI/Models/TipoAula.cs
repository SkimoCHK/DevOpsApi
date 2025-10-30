using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartadoAulasAPI.Models
{
  public class TipoAula
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Nombre {  get; set; }

    [Required]
    public bool Estatus {  get; set; }
  }
}
