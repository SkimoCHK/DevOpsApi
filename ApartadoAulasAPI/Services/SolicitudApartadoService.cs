using ApartadoAulasAPI.DTOs.SolicitudApartado;
using ApartadoAulasAPI.Exceptions;
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using ApartadoAulasAPI.Repositories;
using AutoMapper;
using System;
using System.Runtime.InteropServices;

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

    public async Task<SolicitudApartado> Add(CreateSolicitudApartadoDto dto)
    {
      var solicitud = _mapper.Map<SolicitudApartado>(dto);

      var sonora = GetSonoraTimeZone();

      var nuevaLocalInicio = solicitud.Fecha.ToDateTime(solicitud.HoraInicio);
      var nuevaLocalFin = solicitud.Fecha.ToDateTime(solicitud.HoraFin);

      var nuevaUtcInicio = TimeZoneInfo.ConvertTimeToUtc(nuevaLocalInicio, sonora);
      var nuevaUtcFin = TimeZoneInfo.ConvertTimeToUtc(nuevaLocalFin, sonora);

      var reservas =  _repository.SearchElementsAsync(r =>
          r.AulaId == solicitud.AulaId &&
          r.Fecha == solicitud.Fecha &&
          r.Estado == "Confirmada"
      );

      foreach (var r in reservas)
      {
        var existenteLocalInicio = r.Fecha.ToDateTime(r.HoraInicio);
        var existenteLocalFin = r.Fecha.ToDateTime(r.HoraFin);

        var existenteUtcInicio = TimeZoneInfo.ConvertTimeToUtc(existenteLocalInicio, sonora);
        var existenteUtcFin = TimeZoneInfo.ConvertTimeToUtc(existenteLocalFin, sonora);

        bool hayConflicto = nuevaUtcInicio < existenteUtcFin && nuevaUtcFin > existenteUtcInicio;
        if (hayConflicto)
          throw new HttpException(409, "Ya existe una reserva en ese horario.");
      }

      solicitud.Estado = "Confirmada";
      solicitud.FechaSolicitud = DateTime.UtcNow;

      await _repository.CreateAsync(solicitud);
      await _repository.SaveAsync();

      return solicitud;
    }


    public async Task<IEnumerable<SolicitudApartado>> Get()
      => await _repository.GetAllAsync();
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

    private static TimeZoneInfo GetSonoraTimeZone()
    {
      // Windows: "US Mountain Standard Time" (Sonora no aplica DST)
      // Linux/macOS: "America/Hermosillo"
      string windowsId = "US Mountain Standard Time";
      string ianaId = "America/Hermosillo";
      try
      {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
          return TimeZoneInfo.FindSystemTimeZoneById(windowsId);
        else
          return TimeZoneInfo.FindSystemTimeZoneById(ianaId);
      }
      catch
      {
        // Fallback a UTC si no se encuentra la zona
        return TimeZoneInfo.Utc;
      }
    }
  }
}
