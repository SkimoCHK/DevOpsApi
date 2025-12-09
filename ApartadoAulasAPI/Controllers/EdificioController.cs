using ApartadoAulasAPI.DTOs.Edificio;
using ApartadoAulasAPI.Exceptions;
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApartadoAulasAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EdificioController : ControllerBase
  {
    private readonly ICommonService<Edificio, CreateEdificioDto, UpdateEdificioDto> _service;

    public EdificioController(ICommonService<Edificio, CreateEdificioDto, UpdateEdificioDto> service)
      => _service = service;

    [HttpGet]
    public IActionResult GetMessage()
    {
      return Ok(new { mensaje = "Hola mundo!" });
    }


    [HttpGet]
    public async Task<IActionResult> GetEdificios()
      => Ok(await _service.Get());

    [HttpGet("by/{id}")]
    public async Task<IActionResult> GetEdificioById(int id)
      => Ok(await _service.GetById(id));

    [HttpPost]
    public async Task<IActionResult> CreateEdficio([FromBody] CreateEdificioDto edificio)
    {
      var newEdificio = await _service.Add(edificio);
      return Created(nameof(CreateEdficio), newEdificio);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateEdificio([FromBody] UpdateEdificioDto edificio)
    {
      var updatedEdificio = await _service.Update(edificio);
      return Ok(updatedEdificio);
    }
  }
}