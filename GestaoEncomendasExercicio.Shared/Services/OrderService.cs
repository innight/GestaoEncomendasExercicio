using GestaoEncomendasExercicio.Shared.Interfaces;
using GestaoEncomendasExercicio.Shared.Models;

namespace GestaoEncomendasExercicio.Shared.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<int> CreateOrderAsync(Order order)
        {
            return _orderRepository.CreateOrderAsync(order);
        }

        public Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return _orderRepository.GetAllOrdersAsync();
        }

        public Task<Order?> GetOrderByIdAsync(int id)
        {
            return _orderRepository.GetOrderByIdAsync(id);
        }

        public Task UpdateOrderStatusAsync(int id, string status)
        {
            return _orderRepository.UpdateOrderStatusAsync(id, status);
        }

        public Task CancelOrderAsync(int id)
        {
            return _orderRepository.CancelOrderAsync(id);
        }
    }
}
