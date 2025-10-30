using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using ApartadoAulasAPI.PostgreConfiguration;
using Microsoft.EntityFrameworkCore;

namespace ApartadoAulasAPI.Repositories
{
  public class RolesRepository : IRepository<Roles>
  {
    private readonly AppDbContext _context;

    public Task<Roles> CreateAsync(Roles entity)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<Roles>> GetAllAsync()
        => await _context.Roles.AsNoTracking().ToListAsync();

    public Task<Roles> GetByIdAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task<Roles> SearchElementsAsync(Func<Roles, bool> filter)
    {
      throw new NotImplementedException();
    }

    public Task<Roles> UpdateAsync(Roles entity)
    {
      throw new NotImplementedException();
    }
  }
}
