using ApartadoAulasAPI.DTOs.Auth;
using ApartadoAulasAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApartadoAulasAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private AuthService _service;
    public AuthController(AuthService service)
      => _service = service;

    [HttpPost("Login")]
    public IActionResult LogIn([FromBody] LoginDto loginDto)
    {
      var loginResponse = _service.Login(loginDto);
      return Ok(loginResponse);
    }
  }
}
