using ApartadoAulasAPI.DTOs.Roles;
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using ApartadoAulasAPI.Services;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartadoAulasAPI.Tests
{
  public class RolesServiceTests
  {
    [Fact]
    public async Task Add_ShouldMapAndCallRepositoryCreateAndSave()
    {
      // Arrange
      var repoMock = new Mock<IRepository<Roles>>();
      repoMock.Setup(r => r.CreateAsync(It.IsAny<Roles>())).Returns(Task.CompletedTask);
      repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

      var mapperMock = new Mock<IMapper>();
      var roleEntity = new Roles { Id = 1, Nombre = "Operator" };
      mapperMock.Setup(m => m.Map<Roles>(It.IsAny<CreateRoleDto>())).Returns(roleEntity);

      var service = new RolesService(repoMock.Object, mapperMock.Object);
      var dto = new CreateRoleDto { Nombre = "TestRole", Descripcion = "TestDescription", Clave = "TESTR", Estatus = true};

      // Act
      var result = await service.Add(dto);

      // Assert
      Assert.NotNull(result);
      Assert.Equal("TestRole", result.Nombre);
      repoMock.Verify(r => r.CreateAsync(It.IsAny<Roles>()), Times.Once);
      repoMock.Verify(r => r.SaveAsync(), Times.Once);
    }
  }
}
