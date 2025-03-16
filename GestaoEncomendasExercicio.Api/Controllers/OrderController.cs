using System.Text.Json;
using GestaoEncomendasExercicio.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTestDapper.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    return NotFound($"Encomenda com o ID {id} não foi encontrada.");
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a encomenda com o ID {OrderId}.", id);
                return StatusCode(500, "Ocorreu um erro ao processar o seu pedido.");
            }
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] JsonElement statusJson)
        {
            try
            {
                if (!statusJson.TryGetProperty("status", out JsonElement statusElement) ||
                    statusElement.ValueKind != JsonValueKind.String)
                {
                    return BadRequest("Pedido inválido: o campo 'status' é obrigatório e deve ser uma string.");
                }

                string status = statusElement.GetString()!;
                await _orderService.UpdateOrderStatusAsync(id, status);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar o estado da encomenda com o ID {OrderId}.", id);
                return StatusCode(500, $"Ocorreu um erro ao atualizar o estado: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            try
            {
                await _orderService.CancelOrderAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cancelar a encomenda com o ID {OrderId}.", id);
                return StatusCode(500, "Ocorreu um erro ao cancelar a encomenda.");
            }
        }
    }
}
