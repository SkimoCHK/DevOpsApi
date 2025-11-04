using ApartadoAulasAPI.DTOs.Roles;
using ApartadoAulasAPI.Models;
using AutoMapper;

namespace ApartadoAulasAPI.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<CreateRoleDto, Roles>();

            CreateMap<UpdateRoleDto, Roles>();

        }
    }
}
