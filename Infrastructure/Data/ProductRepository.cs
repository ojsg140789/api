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
            var parameters = new DynamicParameters();
            parameters.Add("p_name", product.Name, DbType.String);
            parameters.Add("p_price", product.Price, DbType.Int32);
            _dbConnection.Execute("CALL sp_save_product(@p_name, @p_price)", parameters);
        }

        public async Task UpdateAsync(Product product)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_id", product.Id, DbType.Int32);
            parameters.Add("p_name", product.Name, DbType.String);
            parameters.Add("p_price", product.Price, DbType.Int32);
            parameters.Add("p_filename", product.Name, DbType.String);
            _dbConnection.Execute("CALL sp_update_product(@p_id, @p_name, @p_price, @p_filename)", parameters);
        }

        public async Task DeleteAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_id", id, DbType.String);
            _dbConnection.Execute("CALL sp_delete_product(@p_id)", parameters);

        }
    }
}
