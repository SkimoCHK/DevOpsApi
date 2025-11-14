using ApartadoAulasAPI.DTOs.Roles;
using ApartadoAulasAPI.Models;
using AutoMapper;

namespace ApartadoAulasAPI.AutoMappers
{
  public class RolesProfile : Profile
  {
    public RolesProfile()
    {
      CreateMap<CreateRoleDto, Roles>();
      CreateMap<UpdateRoleDto, Roles>();
    }
  }
}
