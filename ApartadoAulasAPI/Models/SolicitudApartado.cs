using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartadoAulasAPI.Models
{
  public class SolicitudApartado
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public DateOnly Fecha { get; set; }

    [Required]
    public TimeOnly HoraInicio { get; set; }

    [Required]
    public TimeOnly HoraFin {  get; set; }

    [Required]
    public string Motivo { get; set; }

    [Required]
    public string Estado { get; set; }

    [Required]
    public DateTime FechaSolicitud { get; set; }

    [ForeignKey("Usuario")]
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    [ForeignKey("Aula")]
    public int AulaId { get; set; }
    public Aula Aula { get; set; }

  }
}
