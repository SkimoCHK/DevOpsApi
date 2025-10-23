using ApartadoAulasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ApartadoAulasAPI.PostgreConfiguration
{

  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

  }
}
