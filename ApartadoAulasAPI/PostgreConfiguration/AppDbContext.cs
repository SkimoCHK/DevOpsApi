using ApartadoAulasAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace ApartadoAulasAPI.PostgreConfiguration
{

  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes()){
        entity.SetTableName(entity.DisplayName());
      }
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<Usuario> Usuario{ get; set; }


  }
}
