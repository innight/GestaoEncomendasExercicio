using GestaoEncomendasExercicio.Shared.Models;

namespace GestaoEncomendasExercicio.Shared.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<bool> UpdateOrderStatusAsync(int id, string newStatus);
        Task<bool> CancelOrderAsync(int id);
    }
}
