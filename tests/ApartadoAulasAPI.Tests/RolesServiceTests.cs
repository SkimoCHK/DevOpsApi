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
      var roleEntity = new Roles { Id = 1, Nombre = "TestRole" };
      mapperMock.Setup(m => m.Map<Roles>(It.IsAny<CreateRoleDto>())).Returns(roleEntity);

      var service = new RolesService(repoMock.Object, mapperMock.Object);
      var dto = new CreateRoleDto { Nombre = "TestRole", Descripcion = "TestDescription", Clave = "TESTR", Estatus = true };

      // Act
      var result = await service.Add(dto);

      // Assert
      Assert.NotNull(result);
      Assert.Equal("TestRole", result.Nombre);
      repoMock.Verify(r => r.CreateAsync(It.IsAny<Roles>()), Times.Once);
      repoMock.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task Update_ShouldReturnUpdatedRole_WhenExists()
    {
      // Arrange
      var repoMock = new Mock<IRepository<Roles>>();
      var mapperMock = new Mock<IMapper>();

      var existingRole = new Roles { Id = 1, Nombre = "OldName", Descripcion = "OldDesc" };
      var updatedRole = new Roles { Id = 1, Nombre = "NewName", Descripcion = "NewDesc" };

      repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(existingRole);
      repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

      mapperMock
          .Setup(m => m.Map(It.IsAny<UpdateRoleDto>(), It.IsAny<Roles>()))
          .Returns(updatedRole);

      var service = new RolesService(repoMock.Object, mapperMock.Object);
      var dto = new UpdateRoleDto
      {
        Id = 1,
        Nombre = "NewName",
        Descripcion = "NewDesc",
        Estatus = true,
        Clave = "NEWC"
      };

      // Act
      var result = await service.Update(dto);

      // Assert
      Assert.NotNull(result);
      Assert.Equal("NewName", result.Nombre);
      repoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
      repoMock.Verify(r => r.UpdateAsync(It.IsAny<Roles>()), Times.Once);
      repoMock.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task Update_ShouldReturnNull_WhenRoleNotFound()
    {
      // Arrange
      var repoMock = new Mock<IRepository<Roles>>();
      repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Roles)null);
      var mapperMock = new Mock<IMapper>();

      var service = new RolesService(repoMock.Object, mapperMock.Object);
      var dto = new UpdateRoleDto
      {
        Id = 99,
        Nombre = "DoesNotExist",
        Descripcion = "MissingRole"
      };

      // Act
      var result = await service.Update(dto);

      // Assert
      Assert.Null(result);
      repoMock.Verify(r => r.UpdateAsync(It.IsAny<Roles>()), Times.Never);
      repoMock.Verify(r => r.SaveAsync(), Times.Never);
    }

    [Fact]
    public async Task GetById_ShouldReturnCorrectRole()
    {
      // Arrange
      var repoMock = new Mock<IRepository<Roles>>();
      var mapperMock = new Mock<IMapper>();
      var expectedRole = new Roles { Id = 5, Nombre = "Admin" };

      repoMock.Setup(r => r.GetByIdAsync(5)).ReturnsAsync(expectedRole);

      var service = new RolesService(repoMock.Object, mapperMock.Object);

      // Act
      var result = await service.GetById(5);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(5, result.Id);
      Assert.Equal("Admin", result.Nombre);
    }
  }
}
