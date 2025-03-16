using GestaoEncomendasExercicio.Shared.Interfaces;
using GestaoEncomendasExercicio.Shared.Models;
using GestaoEncomendasExercicio.Shared.Services;
using Moq;

namespace WebApiTestDapper.Test
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _mockRepository;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _mockRepository = new Mock<IOrderRepository>();
            _orderService = new OrderService(_mockRepository.Object);
        }


        [Fact]
        public async Task CreateOrderAsync_ShouldReturnOrderId_WhenOrderIsValid()
        {
            // Arrange
            var order = new Order
            {
                CustomerName = "Alice",
                Address = "100 Main Street",
                Status = "Pendente",
                CreatedAt = DateTime.UtcNow
            };

            var expectedOrderId = 42;

            _mockRepository
                .Setup(repo => repo.CreateOrderAsync(order))
                .ReturnsAsync(expectedOrderId);

            // Act
            var result = await _orderService.CreateOrderAsync(order);

            // Assert
            Assert.Equal(expectedOrderId, result);
            _mockRepository.Verify(repo => repo.CreateOrderAsync(order), Times.Once);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ShouldReturnOrder_WhenOrderExists()
        {
            // Arrange
            var orderId = 1;
            var expectedOrder = new Order
            {
                Id = orderId,
                CustomerName = "Jane Doe",
                Address = "123 Main St",
                Status = "Despachada",
                CreatedAt = DateTime.UtcNow
            };

            _mockRepository
                .Setup(repo => repo.GetOrderByIdAsync(orderId))
                .ReturnsAsync(expectedOrder);

            // Act
            var result = await _orderService.GetOrderByIdAsync(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.Id);
            Assert.Equal("Jane Doe", result.CustomerName);
            Assert.Equal("Despachada", result.Status);
            _mockRepository.Verify(repo => repo.GetOrderByIdAsync(orderId), Times.Once);
        }

        [Fact]
        public async Task GetOrderByIdAsync_OrderDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var orderId = 999;

            _mockRepository
                .Setup(repo => repo.GetOrderByIdAsync(orderId))
                .ReturnsAsync((Order)null);

            // Act
            var result = await _orderService.GetOrderByIdAsync(orderId);

            // Assert
            Assert.Null(result);
            _mockRepository.Verify(repo => repo.GetOrderByIdAsync(orderId), Times.Once);
        }

        [Fact]
        public async Task GetAllOrdersAsync_ShouldReturnListOfOrders()
        {
            // Arrange
            var expectedOrders = new List<Order>
        {
            new Order { Id = 1, CustomerName = "John Doe", Address = "100 First St", Status = "Pendente", CreatedAt = DateTime.UtcNow },
            new Order { Id = 2, CustomerName = "Alice Smith", Address = "200 Second St", Status = "Entregue", CreatedAt = DateTime.UtcNow }
        };

            _mockRepository
                .Setup(repo => repo.GetAllOrdersAsync())
                .ReturnsAsync(expectedOrders);

            // Act
            var result = await _orderService.GetAllOrdersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Collection(result,
                order => Assert.Equal("John Doe", order.CustomerName),
                order => Assert.Equal("Alice Smith", order.CustomerName));

            _mockRepository.Verify(repo => repo.GetAllOrdersAsync(), Times.Once);
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnOrder_WhenOrderExists()
        {
            // Arrange
            var orderId = 1;
            var order = new Order { Id = orderId, CustomerName = "John Doe", Address = "123 Main St", Status = "Pendente", CreatedAt = DateTime.UtcNow };
            _mockRepository.Setup(repo => repo.GetOrderByIdAsync(orderId)).ReturnsAsync(order);

            // Act
            var result = await _orderService.GetOrderByIdAsync(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.Id);
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = 99;
            _mockRepository.Setup(repo => repo.GetOrderByIdAsync(orderId)).ReturnsAsync((Order)null);

            // Act
            var result = await _orderService.GetOrderByIdAsync(orderId);

            // Assert
            Assert.Null(result);
            _mockRepository.Verify(repo => repo.GetOrderByIdAsync(orderId), Times.Once);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldReturnOrderId()
        {
            // Arrange
            var order = new Order
            {
                CustomerName = "John Doe",
                Address = "123 Main St",
                Status = "Pendente",
                CreatedAt = DateTime.UtcNow
            };
            _mockRepository.Setup(repo => repo.CreateOrderAsync(order)).ReturnsAsync(1);

            // Act
            var result = await _orderService.CreateOrderAsync(order);

            // Assert
            Assert.Equal(1, result);
            _mockRepository.Verify(repo => repo.CreateOrderAsync(order), Times.Once);
        }

        [Fact]
        public async Task GetAllOrders_ShouldReturnListOfOrders()
        {
            // Arrange
            var orders = new List<Order>
        {
            new Order { Id = 1, CustomerName = "John Doe", Address = "123 Main St", Status = "Pendente", CreatedAt = DateTime.UtcNow },
            new Order { Id = 2, CustomerName = "Jane Doe", Address = "456 Elm St", Status = "Entregue", CreatedAt = DateTime.UtcNow }
        };
            _mockRepository.Setup(repo => repo.GetAllOrdersAsync()).ReturnsAsync(orders);

            // Act
            var result = await _orderService.GetAllOrdersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, o => o.CustomerName == "John Doe");
            Assert.Contains(result, o => o.CustomerName == "Jane Doe");
            _mockRepository.Verify(repo => repo.GetAllOrdersAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateOrderStatusAsync_ShouldUpdateStatus()
        {
            // Arrange
            var orderId = 1;
            var newStatus = "Despachada";

            // Act
            await _orderService.UpdateOrderStatusAsync(orderId, newStatus);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateOrderStatusAsync(orderId, newStatus), Times.Once);
        }

        [Fact]
        public async Task CancelOrderAsync_ShouldCancelOrder()
        {
            // Arrange
            var orderId = 1;

            // Act
            await _orderService.CancelOrderAsync(orderId);

            // Assert
            _mockRepository.Verify(repo => repo.CancelOrderAsync(orderId), Times.Once);
        }
    }
}
