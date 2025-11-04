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
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());
            }
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Edificio> Edificio { get; set; }
        public DbSet<TipoAula> TipoAula { get; set; }
        public DbSet<Aula> Aula { get; set; }
        public DbSet<SolicitudApartado> SolicitudApartados { get; set; }
        public DbSet<HistorialAcciones> HistorialAcciones { get; set; }


    }
}
