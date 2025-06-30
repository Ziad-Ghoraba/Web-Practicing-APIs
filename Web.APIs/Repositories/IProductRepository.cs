using Microsoft.EntityFrameworkCore;
using Web.APIs.Models;

namespace Web.APIs.Repositories
{
    public interface IProductRepository
    {
        //Create
        Task CreateAsync(Product product );

        //Read
        Task<IReadOnlyList<Product>> GetAllAsync(); // More safe and only allow "Access by index, Loop, Get the number of elements"

        Task<Product?> GetByIdAsync(int id);

        Task<Product?> GetByNameAsync(string Name);

        //Update
        void Update(Product product);

        //Delete
        public void Delete(Product product);

        //Save
        Task SaveAsync();
    }
}
