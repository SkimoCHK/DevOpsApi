using ApartadoAulasAPI.DTOs.Edificio;
using ApartadoAulasAPI.DTOs.Roles;
using ApartadoAulasAPI.Exceptions;
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using AutoMapper;

namespace ApartadoAulasAPI.Services
{
  public class EdificioService : ICommonService<Edificio, CreateEdificioDto, UpdateEdificioDto>
  {
    public List<string> Errors => throw new NotImplementedException();

    IRepository<Edificio> _repository;
    IMapper _mapper;
    public EdificioService(IRepository<Edificio> repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public async Task<IEnumerable<Edificio>> Get()
      => await _repository.GetAllAsync();

    public async Task<Edificio> GetById(int id)
      => await _repository.GetByIdAsync(id);

    public async Task<Edificio> Add(CreateEdificioDto EdificioDto)
    {
      Validate(EdificioDto);
      var edificio = _mapper.Map<Edificio>(EdificioDto);
      await _repository.CreateAsync(edificio);
      await _repository.SaveAsync();
      return edificio;
    }

    public async Task<Edificio> Update(UpdateEdificioDto EdificioDto)
    {
      var edificio = await _repository.GetByIdAsync(EdificioDto.Id);
      if (edificio == null) throw new HttpsException(404, "No existe el edificio a actualizar");

      Validate(EdificioDto);
      edificio = _mapper.Map<UpdateEdificioDto, Edificio>(EdificioDto,edificio);
      _repository.UpdateAsync(edificio);
      await _repository.SaveAsync();
      return edificio;
    }

    public void Validate(CreateEdificioDto dto)
    {
      var results = _repository.SearchElementsAsync(e => e.Nombre == dto.Nombre);
      if (results.Count() > 0) throw new HttpsException(409, "Ya existe un edificio con este nombre");
    }

    public void Validate(UpdateEdificioDto dto)
    {
      var results = _repository.SearchElementsAsync(e => e.Nombre == dto.Nombre && e.Id != dto.Id);
      if (results.Count() > 0) throw new HttpsException(409, "Ya existe un edificio con este nombre");
    }
  }
}
