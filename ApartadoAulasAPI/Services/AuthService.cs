using ApartadoAulasAPI.DTOs.Auth;
using ApartadoAulasAPI.Exceptions;
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using ApartadoAulasAPI.Repositories;
using BC = BCrypt.Net.BCrypt;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace ApartadoAulasAPI.Services
{
  public class AuthService
  {
    private IRepository<Usuario> _repository;
    private SolicitudApartadoRepository _solicitudApartadoRepository;
    public AuthService(IRepository<Usuario> repository, SolicitudApartadoRepository solicitudApartadoRepository)
    {
      _repository = repository;
      _solicitudApartadoRepository = solicitudApartadoRepository;
    }

    public LoginResponse Login(LoginDto loginDto)
    {
      var targetUser = _repository.SearchElementsAsync(u => u.Email == loginDto.Email).FirstOrDefault();
      if (targetUser == null) throw new HttpException(401, "Credenciales incorrectas");
      if (!BC.EnhancedVerify(loginDto.Password, targetUser.Password)) throw new HttpException(401, "Credenciales incorrectas");

      var info = GetInfoUser(targetUser.Id);
      info.IdUsuario = targetUser.Id;
      info.Nombre = $"{targetUser.Nombre} {targetUser.Apellido}";

      return info;
    }

    private LoginResponse GetInfoUser(int idUser)
    {
      var sonoraZone = GetSonoraTimeZone();
      var nowUtc = DateTime.UtcNow;

      var nowSonora = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, sonoraZone);
      var todaySonora = DateOnly.FromDateTime(nowSonora);

      var reservas = _solicitudApartadoRepository.SearchElementsAsync(s => s.UsuarioId == idUser).ToList();

      var totalReservas = reservas.Count;
      var totalActivasHoy = reservas.Count(s => s.Fecha == todaySonora && s.Estado == "Confirmada");

      var proximas = reservas
        .Select(s =>
        {
          var localSonora = s.Fecha.ToDateTime(s.HoraInicio);
          DateTime utcInstant;
          try
          {
            utcInstant = TimeZoneInfo.ConvertTimeToUtc(localSonora, sonoraZone);
          }
          catch
          {
            utcInstant = DateTime.SpecifyKind(localSonora, DateTimeKind.Utc);
          }
          return new { Reserva = s, UtcInstant = utcInstant };
        })
        .Where(x => x.UtcInstant >= nowUtc)   
        .OrderBy(x => x.UtcInstant)
        .Take(2)
        .Select(x => x.Reserva)
        .ToList();

      return new LoginResponse
      {
        IdUsuario = idUser,
        Nombre = string.Empty, 
        TotalReservas = totalReservas,
        TotalActivasHoy = totalActivasHoy,
        ProximasReservas = proximas
      };
    }

    private static TimeZoneInfo GetSonoraTimeZone()
    {
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
        return TimeZoneInfo.Utc;
      }
    }


  }
}
