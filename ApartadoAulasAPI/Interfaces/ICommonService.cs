namespace ApartadoAulasAPI.Interfaces
{
  public interface ICommonService<T, TI, TU>
  {
    public List<string> Errors { get; }

    Task<IEnumerable<T>> Get();

    Task<T> GetById(int id);

    Task<T> Add(TI beerDTO);

    Task<T> Update(int id, TU beerDTO);

    bool Validate(TI dto);

    bool Validate(TU dto);
  }
}
