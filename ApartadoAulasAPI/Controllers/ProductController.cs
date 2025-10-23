using ApartadoAulasAPI.Interfaces;
using ApartadoAulasAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApartadoAulasAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    IProductService _service;
    public ProductController(IProductService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody]Product product)
    {
      var newProduct = await _service.AddProduct(product);
      return Created(nameof(CreateProduct), newProduct);
    }
  }
}
