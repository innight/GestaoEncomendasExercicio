using System.Data;
using Dapper;
using GestaoEncomendasExercicio.Shared.Interfaces;
using GestaoEncomendasExercicio.Shared.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace GestaoEncomendasExercicio.Shared.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string is missing.");
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<int> CreateOrderAsync(Order order)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CustomerName", order.CustomerName);
            parameters.Add("@Address", order.Address);
            parameters.Add("@Status", order.Status);
            parameters.Add("@CreatedAt", order.CreatedAt);

            using var connection = CreateConnection();

            return await connection.ExecuteScalarAsync<int>("usp_CreateOrder", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Order>("usp_GetAllOrders", commandType: CommandType.StoredProcedure);
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            using var connection = CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            return await connection.QuerySingleOrDefaultAsync<Order>("usp_GetOrderById", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, string newStatus)
        {
            using var connection = CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            parameters.Add("@Status", newStatus);

            var rowsAffected = await connection.ExecuteAsync("usp_UpdateOrderStatus", parameters, commandType: CommandType.StoredProcedure);

            return rowsAffected > 0;
        }

        public async Task<bool> CancelOrderAsync(int id)
        {
            using var connection = CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            var rowsAffected = await connection.ExecuteAsync("usp_CancelOrder", parameters, commandType: CommandType.StoredProcedure);

            return rowsAffected > 0;
        }
    }
}
