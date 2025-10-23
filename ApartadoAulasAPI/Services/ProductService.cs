using ApartadoAulasAPI.DTOs;
using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using ApartadoAulasAPI.PostgreConfiguration;
using Microsoft.EntityFrameworkCore;

namespace ApartadoAulasAPI.Services
{
  public class ProductService : IProductService
  {
    readonly AppDbContext _context;
    public ProductService(AppDbContext context) => _context = context;

    public async Task<Product> AddProduct(Product newProduct)
    {
      await _context.AddAsync(newProduct);
      await _context.SaveChangesAsync();
      return newProduct;    
      
    }

    public async Task<Product> GetProductById(int id)
    {
      var producto = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
      return producto;
    }

    public Task<IEnumerable<Product>> GetProducts()
    {
      throw new NotImplementedException();
    }
  }
}
