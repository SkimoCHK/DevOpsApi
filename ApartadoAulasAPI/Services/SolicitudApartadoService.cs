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

    public IEnumerable<SolicitudApartado> GetHistorialReservas(int idUser)
    {
      var reservas = _repository.SearchElementsAsync(r => r.UsuarioId == idUser);
      return reservas;
    }

    public async Task<SolicitudApartado> Add(CreateSolicitudApartadoDto dto)
    {
      var solicitud = _mapper.Map<SolicitudApartado>(dto);

      var sonora = GetSonoraTimeZone();

      var nuevaLocalInicio = solicitud.Fecha.ToDateTime(solicitud.HoraInicio);
      var nuevaLocalFin = solicitud.Fecha.ToDateTime(solicitud.HoraFin);

      var nuevaUtcInicio = TimeZoneInfo.ConvertTimeToUtc(nuevaLocalInicio, sonora);
      var nuevaUtcFin = TimeZoneInfo.ConvertTimeToUtc(nuevaLocalFin, sonora);

      // Usar el método async que ya filtra en la base de datos
      var reservas = await _repository.GetReservasPorAulaYFecha(solicitud.AulaId, solicitud.Fecha);

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

      try
      {
        await _repository.SaveAsync();
      }
      catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
      {
        // Devuelve la inner exception para depurar (no mantener en producción)
        var detalle = ex.InnerException?.Message ?? ex.Message;
        throw new HttpException(500, $"Error al guardar la reserva: {detalle}");
      }

      return solicitud;
    }

    public async Task<List<DisponibilidadHoraDto>> GetDisponibilidad(int aulaId, DateOnly fecha)
    {
      var reservas = await _repository.GetReservasPorAulaYFecha(aulaId, fecha);

      var disponibilidad = new List<DisponibilidadHoraDto>();

      // Horario escolar: 7:30 - 15:30
      TimeOnly inicioDia = new(7, 30);
      TimeOnly finDia = new(15, 30);

      TimeOnly actual = inicioDia;

      while (actual < finDia)
      {
        TimeOnly siguiente = actual.AddHours(1);

        // Detectar si el intervalo se solapa con alguna reserva confirmada.
        bool ocupado = reservas.Any(r =>
            actual < r.HoraFin && siguiente > r.HoraInicio
        );

        disponibilidad.Add(new DisponibilidadHoraDto
        {
          HoraInicio = actual,
          HoraFin = siguiente,
          Disponible = !ocupado
        });

        actual = siguiente;
      }

      return disponibilidad;
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
