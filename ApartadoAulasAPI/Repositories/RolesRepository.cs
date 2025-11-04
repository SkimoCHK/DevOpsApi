using ApartadoAulasAPI.DTOs.Roles;
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using ApartadoAulasAPI.PostgreConfiguration;
using Microsoft.EntityFrameworkCore;

namespace ApartadoAulasAPI.Repositories
{
  public class RolesRepository : IRepository<Roles>
  {
    private readonly AppDbContext _context;

    public RolesRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Roles>> GetAllAsync()
      => await _context.Roles.AsNoTracking().ToListAsync();

    public async Task<Roles> GetByIdAsync(int id)
      => await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

    public async Task CreateAsync(Roles entity)
      => _context.Roles.Add(entity);

    public void UpdateAsync(Roles entity)
    {
      _context.Roles.Attach(entity);
      _context.Entry(entity).State = EntityState.Modified;
    }

    public IEnumerable<Roles> SearchElementsAsync(Func<Roles, bool> filter)
      => _context.Roles.Where(filter);
    public async Task SaveAsync()
      => await _context.SaveChangesAsync();
  }
}
