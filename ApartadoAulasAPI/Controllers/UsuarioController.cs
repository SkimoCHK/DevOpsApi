using ApartadoAulasAPI.DTOs.Usuario;
using ApartadoAulasAPI.Exceptions;
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApartadoAulasAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsuarioController : ControllerBase
  {
    private ICommonService<Usuario, CreateUserDto, UpdateUserDto> _service;

    public UsuarioController(ICommonService<Usuario, CreateUserDto, UpdateUserDto> service)
      => _service = service;

    [HttpGet("GetError")]
    public IActionResult Test()
    {

      throw new Exception("Error de prueba");


    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
      => Ok(await _service.Get());

    [HttpPost("AddUser")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
      var newUserr = await _service.Add(userDto);
      return Created(nameof(CreateUser), newUserr);
    }

    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto userDto)
    {
      var userModifed = await _service.Update(userDto);
      return Ok(userModifed);
    }
  }
}
