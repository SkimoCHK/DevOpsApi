using ApartadoAulasAPI.DTOs.SolicitudApartado;
using ApartadoAulasAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApartadoAulasAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SolicitudApartadoController : ControllerBase
  {
    private SolicitudApartadoService _service;
    public SolicitudApartadoController(SolicitudApartadoService service)
      => _service = service;

    [HttpPost("CreateSolicitud")]
    public async Task<IActionResult> CreateSolicitud([FromBody] CreateSolicitudApartadoDto entity)
    {
      var solicitud = await _service.Add(entity);
      return Created(nameof(CreateSolicitud), solicitud);
    }

    [HttpGet("GetReservas")]
    public async Task<IActionResult> GetReservas()
      => Ok(await _service.Get());

    [HttpGet("Disponibilidad")]
    public async Task<IActionResult> GetDisponibilidad(int aulaId, DateOnly fecha)
    {
      var result = await _service.GetDisponibilidad(aulaId, fecha);
      return Ok(result);
    }

    [HttpGet("GetHistorialReservas")]
    public async Task<IActionResult> GetReservas(int userId)
    {
      var reservas = _service.GetHistorialReservas(userId);
      return Ok(reservas);
    }

  }
}
