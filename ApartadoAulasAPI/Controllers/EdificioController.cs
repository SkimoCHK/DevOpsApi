using ApartadoAulasAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApartadoAulasAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EdificioController : ControllerBase
  {
    [HttpGet]
    public IActionResult GetEdificios()
    {
      var edificios = new List<Edificio>()
      {
        new Edificio
        {
          Id = 1,
          Nombre = "Edificio 1",
          Estatus = true
        },
        new Edificio
        {
          Id = 2,
          Nombre = "Edificio 2",
          Estatus = true
        },
        new Edificio
        {
          Id = 3,
          Nombre = "Edificio H",
          Estatus = true
        },
        new Edificio
        {
          Id = 4,
          Nombre = "Taller pesado",
          Estatus = true
        },
        new Edificio
        {
          Id = 5,
          Nombre = "Edificio 4"
        }


      };
      return Ok(edificios);
    }
  }
}
