using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using ApartadoAulasAPI.PostgreConfiguration;
using Microsoft.EntityFrameworkCore;

namespace ApartadoAulasAPI.Repositories
{
  public class EdificioRepository : IRepository<Edificio>
  {
    private readonly AppDbContext _context;

    public EdificioRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Edificio>> GetAllAsync()
      => await _context.Edificio.Include(e => e.Encargado)
      .AsNoTracking()
      .ToListAsync();

    public async Task<Edificio> GetByIdAsync(int id)
      => await _context.Edificio.FirstOrDefaultAsync(e => e.Id == id);

    public async Task CreateAsync(Edificio entity)
      => await _context.Edificio.AddAsync(entity);

    public void UpdateAsync(Edificio entity)
    {
      _context.Edificio.Attach(entity);
      _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task SaveAsync()
      => await _context.SaveChangesAsync();
    public IEnumerable<Edificio> SearchElementsAsync(Func<Edificio, bool> filter)
    {
      var elements = _context.Edificio.Where(filter);
      return elements;
    }


  }
}
