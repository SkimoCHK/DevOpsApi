using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;

namespace ApartadoAulasAPI.Repositories
{
  public class UserRepository : IRepository<Usuario>
  {

    public Task<IEnumerable<Usuario>> GetAllAsync()
    {
      throw new NotImplementedException();
    }

    public Task<Usuario> GetByIdAsync(int id)
    {
      throw new NotImplementedException();
    }
    public Task CreateAsync(Usuario entity)
    {
      throw new NotImplementedException();
    }
    public void UpdateAsync(Usuario entity)
    {
      throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Usuario> SearchElementsAsync(Func<Usuario, bool> filter)
    {
      throw new NotImplementedException();
    }


  }
}
