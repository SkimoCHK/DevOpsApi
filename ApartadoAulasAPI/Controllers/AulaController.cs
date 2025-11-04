using ApartadoAulasAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApartadoAulasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AulaController : ControllerBase
    {
        public AulaController() { }

        [HttpGet]
        public IActionResult GetAulas()
        {
            var aulas = new List<Aula>
        {
          new Aula{Id=1, Nombre = "Laboratorio 1101", CapacidadEstudiantes = 30, Estatus = true},
          new Aula{Id=2, Nombre = "Laboratorip 1103", CapacidadEstudiantes= 15, Estatus= true},
          new Aula{Id=3, Nombre = "Lab. Cisco", CapacidadEstudiantes=25, Estatus = true},
          new Aula{Id=4, Nombre = "Lab. Microsoft", CapacidadEstudiantes=25, Estatus = true},
        };

            return Ok(aulas);
        }
    }
}
