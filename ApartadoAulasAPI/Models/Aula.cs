using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartadoAulasAPI.Models
{
    public class Aula
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        public string? Descripcion { get; set; }

        [Required]
        public int CapacidadEstudiantes { get; set; }

        [Required]
        public bool Estatus { get; set; }

        [ForeignKey("TipoAula")]
        public int TipoAulaId { get; set; }
        public TipoAula TipoAula { get; set; }

        [ForeignKey("Edificio")]
        public int EdificioId { get; set; }
        public Edificio Edificio { get; set; }

    }
}
