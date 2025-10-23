using ApartadoAulasAPI.DTOs;
using ApartadoAulasAPI.Models;

namespace ApartadoAulasAPI.Interfaces
{
  public interface IProductService
  {
    Task<Product> AddProduct(Product productDto);
    Task<IEnumerable<Product>> GetProducts();
  }
}
