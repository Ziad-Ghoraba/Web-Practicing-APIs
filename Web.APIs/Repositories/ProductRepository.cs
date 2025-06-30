using Microsoft.EntityFrameworkCore;
using Web.APIs.Models;

namespace Web.APIs.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Create
        public async Task CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        //Read
        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product?> GetByNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }

        //Update
        public void Update(Product product)
        {
            _context.Update(product);
        }

        //Delete
        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        //Save
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
