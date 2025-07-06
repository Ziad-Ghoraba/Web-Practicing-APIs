using Web.APIs.Models;

namespace Web.APIs.DTO
{
    public class CategoryWithProductsDTO
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public List<ProductDTO>? ProductsDTO { get; set; }
    }
}
