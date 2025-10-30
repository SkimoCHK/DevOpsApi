using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartadoAulasAPI.Models
{
  public class HistorialAcciones
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(120)]
    public string Accion {  get; set; }

    public string? Comentario { get; set; }

    public DateTime FechaAccion { get; set; }

    [ForeignKey("Solicitud")]
    public int SolicitudId { get; set; }
    public SolicitudApartado Solicitud { get; set; }

    [ForeignKey("Usuario")]
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }


  }
}
