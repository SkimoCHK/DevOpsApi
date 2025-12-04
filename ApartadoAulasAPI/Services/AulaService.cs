using ApartadoAulasAPI.DTOs.Aula;
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;

namespace ApartadoAulasAPI.Services
{
  public class AulaService : ICommonService<Aula, CreateAulaDto, UpdateAulaDto>
  {
    private readonly IRepository<Aula> _aulaRepository;

    public AulaService(IRepository<Aula> aulaRepository)
    {
      _aulaRepository = aulaRepository;
    }

    public List<string> Errors => throw new NotImplementedException();

    public Task<Aula> Add(CreateAulaDto CreateEntityDto)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<Aula>> Get()
      => await _aulaRepository.GetAllAsync();
    public Task<Aula> GetById(int id)
    {
      throw new NotImplementedException();
    }

    public Task<Aula> Update(UpdateAulaDto UpdateEntityDto)
    {
      throw new NotImplementedException();
    }

    public void Validate(CreateAulaDto dto)
    {
      throw new NotImplementedException();
    }

    public void Validate(UpdateAulaDto dto)
    {
      throw new NotImplementedException();
    }
  }
}
