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
      try
      {
        var role = _mapper.Map<Roles>(roleInsertDto);
        //Validate(roleInsertDto);
        await _rolesRepository.CreateAsync(role);
        await _rolesRepository.SaveAsync();
        return role;
      }
      catch (HttpsException ex)
      {
        throw ex;
      }
      catch (Exception ex){
        throw new HttpsException();
      }

    }

    public async Task<Roles> Update(UpdateRoleDto roleUpdateDto)
    {
      var role = await _rolesRepository.GetByIdAsync(roleUpdateDto.Id);
      if (role == null) return null;

      role = _mapper.Map<UpdateRoleDto, Roles>(roleUpdateDto, role);
      _rolesRepository.UpdateAsync(role);

      await _rolesRepository.SaveAsync();
      return role;
    }

    public void Validate(CreateRoleDto dto)
    {
      var exception = dto switch
      {
        null => new HttpsException(409, "Petición inválida"),
        { Nombre: null or "", Clave: null or ""} => new HttpsException(409, "Nombre y clave son campos obligatorios"),
        { Nombre: null or ""} => new HttpsException(409, "El nombre es un campo obligatorio"),
        { Clave: null or ""} => new HttpsException(409, "La clave es un campo obligatorio"),
        _ => null
      };
      if (exception is not null) throw exception;
    }

    public bool Validate(UpdateRoleDto dto)
    {
      throw new NotImplementedException();
    }

  }
}
