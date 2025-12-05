using ApartadoAulasAPI.DTOs.SolicitudApartado;
using ApartadoAulasAPI.Exceptions;
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using ApartadoAulasAPI.Repositories;
using AutoMapper;

namespace ApartadoAulasAPI.Services
{
  public class SolicitudApartadoService : ICommonService<SolicitudApartado, CreateSolicitudApartadoDto, UpdatedSolicitudApartadoDto>
  {
    private SolicitudApartadoRepository _repository;
    private IMapper _mapper;
    public SolicitudApartadoService(SolicitudApartadoRepository repository, IMapper mapper)
    {
      _mapper = mapper;
      _repository = repository;
    }
    public List<string> Errors => throw new NotImplementedException();

    public async Task<SolicitudApartado> Add(CreateSolicitudApartadoDto CreateEntityDto)
    {
      var solicitud = _mapper.Map<SolicitudApartado>(CreateEntityDto);

      var solicitudes = await _repository.GetAllAsync();

      foreach (var s in solicitudes){
        if (s.Estado != "Finalizada" || s.AulaId != solicitud.AulaId) continue;

        if (solicitud.Fecha == s.Fecha && (solicitud.HoraInicio == s.HoraInicio || solicitud.HoraInicio < s.HoraFin)) 
          throw new HttpException(409,"Ya existe una reserva con esa fecha");
      
      }

      solicitud.Estado = "Aprobada";
      solicitud.FechaSolicitud = DateTime.Now.ToUniversalTime();
      await _repository.CreateAsync(solicitud);
      await _repository.SaveAsync();
      return solicitud;
    }

    public Task<IEnumerable<SolicitudApartado>> Get()
    {
      throw new NotImplementedException();
    }

    public Task<SolicitudApartado> GetById(int id)
    {
      throw new NotImplementedException();
    }

    public Task<SolicitudApartado> Update(UpdatedSolicitudApartadoDto UpdateEntityDto)
    {
      throw new NotImplementedException();
    }

    public void Validate(CreateSolicitudApartadoDto dto)
    {
      throw new NotImplementedException();
    }

    public void Validate(UpdatedSolicitudApartadoDto dto)
    {
      throw new NotImplementedException();
    }
  }
}
