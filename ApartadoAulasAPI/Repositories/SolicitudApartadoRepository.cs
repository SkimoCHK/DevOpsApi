using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using ApartadoAulasAPI.PostgreConfiguration;
using Microsoft.EntityFrameworkCore;

namespace ApartadoAulasAPI.Repositories
{
  public class SolicitudApartadoRepository : IRepository<SolicitudApartado>
  {
    private readonly AppDbContext _context;
    public SolicitudApartadoRepository(AppDbContext context)
      => _context = context;
    
    public async Task CreateAsync(SolicitudApartado entity)
      => await _context.SolicitudApartados.AddAsync(entity);

    public async Task<IEnumerable<SolicitudApartado>> GetAllAsync()
      => await _context.SolicitudApartados.AsNoTracking().ToListAsync();

    public Task<SolicitudApartado> GetByIdAsync(int id)
    {
      throw new NotImplementedException();
    }

    public async Task SaveAsync()
      => await _context.SaveChangesAsync();

    public IEnumerable<SolicitudApartado> SearchElementsAsync(Func<SolicitudApartado, bool> filter)
    {
      throw new NotImplementedException();
    }

    public void UpdateAsync(SolicitudApartado entity)
    {
      throw new NotImplementedException();
    }
  }
}
