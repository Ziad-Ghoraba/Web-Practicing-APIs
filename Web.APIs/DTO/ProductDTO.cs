using System.ComponentModel.DataAnnotations;

namespace Web.APIs.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }
    }
}
