
using LojaOnline.Domain.Entities;

namespace LojaOnline.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<int> AddAsync(Product product);

        Task<Product> GetByIdAsync(int id);

        Task<bool> DeleteAsync(int id);

        Task<Product?> UpdateAsync(Product product);
    }
}
