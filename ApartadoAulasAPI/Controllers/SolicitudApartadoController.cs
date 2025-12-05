using ApartadoAulasAPI.DTOs.SolicitudApartado;
using ApartadoAulasAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

  }
}
