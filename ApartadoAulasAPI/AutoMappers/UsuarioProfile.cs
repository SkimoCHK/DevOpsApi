using ApartadoAulasAPI.DTOs.Usuario;
using ApartadoAulasAPI.Models;
using AutoMapper;

namespace ApartadoAulasAPI.AutoMappers
{
  public class UsuarioProfile : Profile
  {
    public UsuarioProfile()
    {
      CreateMap<CreateUserDto, Usuario>();
      CreateMap<UpdateUserDto, Usuario>();
    }
  }
}
