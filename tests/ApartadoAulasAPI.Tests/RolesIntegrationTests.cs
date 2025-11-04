using ApartadoAulasAPI.AutoMappers;
using ApartadoAulasAPI.DTOs.Roles;
using ApartadoAulasAPI.PostgreConfiguration;
using ApartadoAulasAPI.Repositories;
using ApartadoAulasAPI.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace ApartadoAulasAPI.Tests
{
  public class RolesIntegrationTests
  {
    private IMapper BuildMapper()
    {
      var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
      return config.CreateMapper();
    }

    [Fact]
    public async Task AddRole_ThenGetById_ReturnsAddedRole()
    {
      var options = new DbContextOptionsBuilder<AppDbContext>()
          .UseInMemoryDatabase(databaseName: "Roles_Test_Db")
          .Options;

      using var context = new AppDbContext(options);
      var repo = new RolesRepository(context);
      var mapper = BuildMapper();
      var service = new RolesService(repo, mapper);

      // Act
      var created = await service.Add(new CreateRoleDto { Nombre = "IntegrationRole", Clave = "INT_ROLE", Descripcion = "Rol de prueba de integracion"});
      var fetched = await service.GetById(created.Id);

      // Assert
      Assert.NotNull(fetched);
      Assert.Equal("IntegrationRole", fetched.Nombre);
    }
  }
}
