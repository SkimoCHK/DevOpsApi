using ApartadoAulasAPI.DTOs.Usuario;
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using System.Security.Cryptography.X509Certificates;

namespace ApartadoAulasAPI.Services
{
  public class UsuarioService : ICommonService<Usuario, CreateUserDto, UpdateUserDto>
  {
    private readonly IRepository<Usuario> _service;
    private readonly IMapper _mapper;

    public List<string> Errors => throw new NotImplementedException();

    public UsuarioService(IRepository<Usuario> service, IMapper mapper)
    {
      _service = service;
      _mapper = mapper;
    }


    public async Task<IEnumerable<Usuario>> Get()
       => await _service.GetAllAsync();

    public async Task<Usuario> GetById(int id)
      => await _service.GetByIdAsync(id);

    public async Task<Usuario> Add(CreateUserDto UserDto)
    {
      var newUser = _mapper.Map<Usuario>(UserDto);
      newUser.Password = BC.EnhancedHashPassword(newUser.Password);
      await _service.CreateAsync(newUser);
      await _service.SaveAsync();
      return newUser;
    }

    public async Task<Usuario> Update(UpdateUserDto UserDto)
    {

      var user = await _service.GetByIdAsync(UserDto.Id);
      if (user == null) return null;

      //Confirmamos que la contraseña realmente haya cambiado
      //Si devuelve true significa que la nueva contraseña no coincide con el hash en la base de datos
      //Por ende significa que se actualizo la password
      if (!BC.EnhancedVerify(UserDto.Password, user.Password))
      {
        //Si esta si cambio, generamos un nuevo hash
        UserDto.Password = BC.EnhancedHashPassword(UserDto.Password);
      }
      UserDto.Password = user.Password;
      user = _mapper.Map<UpdateUserDto, Usuario>(UserDto, user);

      _service.UpdateAsync(user);
      await _service.SaveAsync();
      return user;
      
    }

    public void Validate(CreateUserDto dto)
    {
      throw new NotImplementedException();
    }

    public void Validate(UpdateUserDto dto)
    {
      throw new NotImplementedException();
    }

  }
}
