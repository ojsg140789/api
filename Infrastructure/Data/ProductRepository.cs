using Core.Entities;
using Core.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProductRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var sql = "SELECT * FROM Products";
            return await _dbConnection.QueryAsync<Product>(sql);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
        }

        public async Task AddAsync(Product product)
        {
            var sql = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)";
            await _dbConnection.ExecuteAsync(sql, product);
        }

        public async Task UpdateAsync(Product product)
        {
            var sql = "UPDATE Products SET Name = @Name, Price = @Price, FileName = @FileName WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, product);
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
