using ApartadoAulasAPI.DTOs.Roles;
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApartadoAulasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ICommonService<Roles, CreateRoleDto, UpdateRoleDto> _service;

        public RolesController(ICommonService<Roles, CreateRoleDto, UpdateRoleDto> service) => _service = service;


        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _service.Get();
            return Ok(roles);
        }

        [HttpGet("by/id")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _service.GetById(id);
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto)
        {
            var newRole = await _service.Add(createRoleDto);
            return Created(nameof(CreateRole), newRole);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoleDto([FromBody] UpdateRoleDto updateRoleDto)
        {
            var updatedRole = await _service.Update(updateRoleDto);
            return Ok(updatedRole);
        }
    }
}
