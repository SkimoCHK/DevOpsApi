namespace ApartadoAulasAPI.Interfaces
{
  public interface IRepository<TEntity> where TEntity : class
  {
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task CreateAsync(TEntity entity);
    void UpdateAsync(TEntity entity);
    IEnumerable<TEntity> SearchElementsAsync(Func<TEntity, bool> filter);
    Task SaveAsync();

  }
}
