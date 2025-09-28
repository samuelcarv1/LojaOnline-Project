using System.Data;
using Dapper;
using LojaOnline.Domain.Entities;
using LojaOnline.Domain.Repositories;


namespace LojaOnline.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public ProductRepository(IDbConnection connection) 
        {
            _connection = connection;
        }

        public async Task<int> AddAsync(Product product)
        {
            var sql = @"Insert Products (NM_Name, Price)
                    Values (@Name, @Price);
                    Select Cast(Scope_Identity() as int);";

            var id = await _connection.QuerySingleAsync<int>(sql, new { product.Name, product.Price });
            product.Id = id;

            return id;

        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var sql = "Select ID_Product as Id, NM_Name As Name, Price From Products";
            return await _connection.QueryAsync<Product>(sql);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var sql = "Select ID_Product as Id, NM_Name As Name, Price From Products Where ID_Product = @id";

            return await _connection.QuerySingleAsync<Product>(sql, new { id });
        }
    }
}
