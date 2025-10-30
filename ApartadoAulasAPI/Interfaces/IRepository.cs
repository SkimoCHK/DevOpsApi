namespace ApartadoAulasAPI.Interfaces
{
  public interface IRepository<TEntity>
  {
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> SearchElementsAsync(Func<TEntity, bool> filter);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);

  }
}
