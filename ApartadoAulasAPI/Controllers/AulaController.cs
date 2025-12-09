using ApartadoAulasAPI.DTOs.Aula;
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApartadoAulasAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AulaController : ControllerBase
  {
    private readonly ICommonService<Aula, CreateAulaDto, UpdateAulaDto> _service;
    public AulaController(ICommonService<Aula, CreateAulaDto, UpdateAulaDto> service)
      => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAulas()
      => Ok(await _service.Get());

        [HttpGet("GetMessage")]
        public IActionResult GetMessage()
            => Ok(new { Message = "Que onda como estan" });
  }
}
