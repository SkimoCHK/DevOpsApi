using ApartadoAulasAPI.DTOs.Edificio;
using ApartadoAulasAPI.DTOs.SolicitudApartado;
using ApartadoAulasAPI.Models;
using AutoMapper;

namespace ApartadoAulasAPI.AutoMappers
{
  public class SolicitudApartadoProfile : Profile
  {
    public SolicitudApartadoProfile()
    {

      CreateMap<CreateSolicitudApartadoDto, SolicitudApartado>();
      CreateMap<UpdatedSolicitudApartadoDto, SolicitudApartado>();

    }
  }
}
