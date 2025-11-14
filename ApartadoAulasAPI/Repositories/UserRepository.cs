using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using ApartadoAulasAPI.PostgreConfiguration;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ApartadoAulasAPI.Repositories
{
  public class UserRepository : IRepository<Usuario>
  {
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Usuario>> GetAllAsync()
      => await _context.Usuario.Include(u => u.Rol)
      .AsNoTracking()
      .ToListAsync();

    public async Task<Usuario> GetByIdAsync(int id)
      => await _context.Usuario.FirstOrDefaultAsync(u => u.Id == id);
    public async Task CreateAsync(Usuario entity)
      => await _context.Usuario.AddAsync(entity);
    public void UpdateAsync(Usuario entity)
    {
      _context.Usuario.Attach(entity);
      _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task SaveAsync() =>
      await _context.SaveChangesAsync();

    public IEnumerable<Usuario> SearchElementsAsync(Func<Usuario, bool> filter)
    {
      throw new NotImplementedException();
    }


  }
}
