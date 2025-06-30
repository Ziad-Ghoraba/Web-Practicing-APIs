using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.APIs.Models;
using Web.APIs.Repositories;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IReadOnlyList<Product> products =  await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            Product? product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");
            return Ok(product);
        }

        [HttpGet("byname")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            Product? product = await _productRepository.GetByNameAsync(name);
            if (product == null)
                return NotFound($"Product with name {name} not found.");
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _productRepository.CreateAsync(product);
            await _productRepository.SaveAsync();
            return CreatedAtAction("GetById", new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            Product? DbProduct = await _productRepository.GetByIdAsync(id);
            if (DbProduct == null)
                return NotFound($"Product with ID {id} not found.");

            DbProduct.Name = product.Name;
            DbProduct.Price = product.Price;
            DbProduct.Description = product.Description;
            DbProduct.CategoryId = product.CategoryId;

            _productRepository.Update(DbProduct);
            await _productRepository.SaveAsync();

            return NoContent(); // 204
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            Product? product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound($"Product with name {id} not found.");

            _productRepository.Delete(product);
            await _productRepository.SaveAsync();
            return NoContent();
        }
    }
}
