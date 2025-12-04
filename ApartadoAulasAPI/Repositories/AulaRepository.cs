using ApartadoAulasAPI.DTOs.Aula;
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using ApartadoAulasAPI.PostgreConfiguration;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ApartadoAulasAPI.Repositories
{
  public class AulaRepository : IRepository<Aula>
  {
    private readonly AppDbContext _context;
    public AulaRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Aula>> GetAllAsync()
      => await _context.Aula.AsNoTracking().ToListAsync();

    public async Task<Aula> GetByIdAsync(int id)
      => await _context.Aula.FirstOrDefaultAsync();
    public async Task CreateAsync(Aula entity)
      => await _context.Aula.AddAsync(entity);

    public void UpdateAsync(Aula entity)
    {
      _context.Aula.Attach(entity);
      _context.Entry(entity).State = EntityState.Modified;
    }

    public IEnumerable<Aula> SearchElementsAsync(Func<Aula, bool> filter)
      => _context.Aula.Where(filter);

    public async Task SaveAsync()
      => await _context.SaveChangesAsync();




  }
}
