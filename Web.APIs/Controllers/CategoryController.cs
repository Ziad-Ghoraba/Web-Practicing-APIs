using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.APIs.DTO;
using Web.APIs.Models;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id )
        {
            CategoryWithProductsDTO categoryWithProducts = new CategoryWithProductsDTO();
            Category? category = _context.Categories.FirstOrDefault(c => c.Id == id);

            List<Product>? products = _context.Products.Where(category => category.Id == id).ToList();
            List<ProductDTO> productsDTO = new();
            foreach (Product product in products)
            {
                ProductDTO productDTO = new ProductDTO();
                productDTO.Id = product.Id;
                productDTO.Name = product.Name;
                productDTO.Description = product.Description;
                productDTO.Price = product.Price;
                productsDTO.Add(productDTO);
            }


            categoryWithProducts.Name = category?.Name;
            categoryWithProducts.Id = category?.Id;
            categoryWithProducts.ProductsDTO = productsDTO;

            GeneralResponse generalResponse = new GeneralResponse();
            generalResponse.Data = categoryWithProducts;
            generalResponse.IsSuccess = true;


            if (category == null)
            {

                generalResponse.IsSuccess = false;
                generalResponse.Data = categoryWithProducts;
            }

            return Ok(generalResponse);

        }
    }
}
