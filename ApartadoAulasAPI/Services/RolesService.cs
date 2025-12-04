using ApartadoAulasAPI.DTOs.Roles;
using ApartadoAulasAPI.Exceptions;
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using AutoMapper;

namespace ApartadoAulasAPI.Services
{
  public class RolesService : ICommonService<Roles, CreateRoleDto, UpdateRoleDto>
  {

    private IRepository<Roles> _rolesRepository;
    private IMapper _mapper;
    public List<string> Errors { get; }

    public RolesService(IRepository<Roles> rolesRepository, IMapper mapper)
    {
      _rolesRepository = rolesRepository;
      _mapper = mapper;
      Errors = new List<string>();
    }

    public async Task<IEnumerable<Roles>> Get()
      => await _rolesRepository.GetAllAsync();

    public async Task<Roles> GetById(int id)
      => await _rolesRepository.GetByIdAsync(id);

    public async Task<Roles> Add(CreateRoleDto roleInsertDto)
    {
      Validate(roleInsertDto);
      var role = _mapper.Map<Roles>(roleInsertDto);
      await _rolesRepository.CreateAsync(role);
      await _rolesRepository.SaveAsync();
      return role;
    }

    public async Task<Roles> Update(UpdateRoleDto roleUpdateDto)
    {
      Validate(roleUpdateDto);
      var role = await _rolesRepository.GetByIdAsync(roleUpdateDto.Id);
      if (role == null) throw new HttpException(404,"Rol a actualizar no encontrado.");

      role = _mapper.Map<UpdateRoleDto, Roles>(roleUpdateDto, role);
      _rolesRepository.UpdateAsync(role);

      await _rolesRepository.SaveAsync();
      return role;
    }

    //En construccion
    public void Validate(CreateRoleDto dto)
    {
      var element = _rolesRepository.SearchElementsAsync(r => r.Nombre == dto.Nombre || r.Clave == dto.Clave).FirstOrDefault();
      if (element == null) return; 

      string message = "";
      if (dto.Nombre == element.Nombre && dto.Clave == element.Clave) message = "El nombre y clave ya están en uso";
      else if (dto.Nombre == element.Nombre) message = $"El nombre '{dto.Nombre}' ya está en uso,";
      else if (dto.Clave == element.Clave) message = $"La clave '{dto.Clave}' ya está en uso.";
      throw new HttpException(409, message);

    }

    public void Validate(UpdateRoleDto dto)
    {
      var element = _rolesRepository.SearchElementsAsync(r => (r.Nombre == dto.Nombre || r.Clave == dto.Clave) && (dto.Id != r.Id)).FirstOrDefault();
      if (element == null) return;

      string message = "";
      if (dto.Nombre == element.Nombre && dto.Clave == element.Clave) message = "El nombre y clave ya están en uso";
      else if (dto.Nombre == element.Nombre) message = $"El nombre '{dto.Nombre}' ya está en uso,";
      else if (dto.Clave == element.Clave) message = $"La clave '{dto.Clave}' ya está en uso.";

      throw new HttpException(409, message);


    }
  }
}