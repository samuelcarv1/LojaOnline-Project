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

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = @"Delete From Products Where ID_Product = @id";
            var rows = await _connection.ExecuteAsync(sql, new { id });

            return rows > 0;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var sql = "Select ID_Product as Id, NM_Name As Name, Price From Products";
            return await _connection.QueryAsync<Product>(sql);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var sql = "Select ID_Product as Id, NM_Name As Name, Price From Products Where ID_Product = @id";

            return await _connection.QuerySingleOrDefaultAsync<Product>(sql, new { id });
        }

        public async Task<Product?> UpdateAsync(Product product)
        {
            var sql = @"Update Products Set NM_Name = @Name, Price = @Price Where ID_Product = @Id;
                        Select ID_Product as Id, NM_Name as Name, Price From Products Where ID_Product = @Id";

            return await _connection.QuerySingleOrDefaultAsync<Product>(sql, new { product.Id, product.Name, product.Price });

        }
    }
}
