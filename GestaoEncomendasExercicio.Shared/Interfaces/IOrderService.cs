using GestaoEncomendasExercicio.Shared.Models;

namespace GestaoEncomendasExercicio.Shared.Interfaces
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task UpdateOrderStatusAsync(int id, string status);
        Task CancelOrderAsync(int id);
    }
}
