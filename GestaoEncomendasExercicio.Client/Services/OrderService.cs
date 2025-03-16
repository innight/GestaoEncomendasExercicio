using System.Net.Http.Json;
using GestaoEncomendasExercicio.Shared.Models;

namespace GestaoEncomendasExercicio.Client.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var orders = await _httpClient.GetFromJsonAsync<List<Order>>("api/orders");
            return orders ?? new List<Order>();
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateOrderStatusAsync(int id, string status)
        {
            var content = JsonContent.Create(new { status });
            var response = await _httpClient.PutAsync($"api/orders/{id}/status", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                return (false, errorMsg);
            }

            return (true, string.Empty);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteOrderAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/orders/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                return (false, errorMsg);
            }

            return (true, string.Empty);
        }
    }
}
