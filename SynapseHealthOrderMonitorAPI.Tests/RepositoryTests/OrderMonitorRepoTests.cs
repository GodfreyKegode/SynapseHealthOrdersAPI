using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SynapseHealthOrderMonitorAPI.Data.Repositories;
using SynapseHealthOrderMonitorAPI.Models;

namespace SynapseHealthOrderMonitorAPI.Tests
{
    public class OrderMonitorRepoTests
    {
        [Fact]
        public async Task UpdateOrderRecordAsync_Success()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OrderMonitorContext>()
                .UseInMemoryDatabase(databaseName: "TestingDatabase")
                .Options;

            /// mock data
            var orders = new List<Order> 
            {
                new Order { OrderId = 1, OrderStatus = "In Progress"},
                new Order { OrderId = 2, OrderStatus = "Delivered"},
                new Order { OrderId = 3, OrderStatus = "Notification Sent"}
            };

            /// save mock data in TestingDatabase
            using (var orderMonitorContext = new OrderMonitorContext(options))
            {
                orderMonitorContext.Orders.AddRange(orders);
                await orderMonitorContext.SaveChangesAsync();
            }

            using (var orderMonitorContext = new OrderMonitorContext(options))
            {
                /// mock logger
                var mockLog = new Mock<ILogger<OrderMonitorRepo>>().Object;
                
                var orderMonitorRepo = new OrderMonitorRepo(mockLog, orderMonitorContext);

                /// mock delivery notification
                var deliveryNotification = new DeliveryNotification
                {
                    NotificationId = 1,
                    OrderId = 2,
                    Message = "",
                    DateTime = DateTime.UtcNow
                };

                // Act
                await orderMonitorRepo.UpdateOrderRecordAsync(deliveryNotification);

                // Assert
                var updatedOrder = orderMonitorContext.Orders
                                        .FirstOrDefault(o => o.OrderId == deliveryNotification.OrderId);
                
                Assert.NotNull(updatedOrder);
                Assert.Equal(2, updatedOrder.OrderId);
                Assert.Equal("Notification Sent", updatedOrder.OrderStatus);
            }            
        }
    }
}