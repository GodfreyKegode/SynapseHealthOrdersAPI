
using SynapseHealthOrderMonitorAPI.Data.Repositories;
using SynapseHealthOrderMonitorAPI.Models;

namespace SynapseHealthOrderMonitorAPI.Services
{
    public class OrderMonitorService : IOrderMonitorService
    {
        private readonly ILogger<OrderMonitorService> _logger;
        private readonly INotificationRepo _notificationRepo;
        private readonly IOrderMonitorRepo _orderMonitorRepo;


        public OrderMonitorService(ILogger<OrderMonitorService> logger, INotificationRepo notificationRepo, IOrderMonitorRepo orderMonitorRepo) 
        {
            _logger = logger;
            _notificationRepo = notificationRepo;
            _orderMonitorRepo = orderMonitorRepo;
        }

        public async Task ProcessOrderAsync(IEnumerable<Order> orders)
        {
            try
            {
                foreach (var order in orders) 
                {
                    if (order.OrderStatus.ToLower() == "delivered")
                    {
                        bool notificationDelivered = await _notificationRepo.SendNotificationAsync(order);

                        if (notificationDelivered)
                        {
                            var deliveryNotification = new DeliveryNotification()
                            {
                                OrderId = order.OrderId,
                                DateTime = DateTime.Now
                            };

                            // update order record
                            await _orderMonitorRepo.UpdateOrderRecordAsync(deliveryNotification);

                            _logger.LogInformation($"Success: Notification sent and Order Id - {order.OrderId} updated");
                        }
                        else
                        {
                            // sent but unable to update record
                            _logger.LogError($"Error: Notification sent but unable to update Order Id - {order.OrderId}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error");
                throw new ApplicationException("Unable to send Process Order from OrderMonitorService", ex);
            }            
        }
    }
}
