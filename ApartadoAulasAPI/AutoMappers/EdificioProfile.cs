using ApartadoAulasAPI.DTOs.Edificio;
using ApartadoAulasAPI.Models;
using AutoMapper;

namespace ApartadoAulasAPI.AutoMappers
{
  public class EdificioProfile : Profile
  {
    public EdificioProfile(){

      CreateMap<CreateEdificioDto, Edificio>();
      CreateMap<UpdateEdificioDto, Edificio>();
    
    }
  }
}
