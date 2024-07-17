using Microsoft.EntityFrameworkCore;
using SynapseHealthOrderMonitorAPI.Models;

namespace SynapseHealthOrderMonitorAPI.Data.Repositories
{
    public class OrderMonitorRepo : IOrderMonitorRepo
    {
        private readonly ILogger<OrderMonitorRepo> _logger;
        private readonly OrderMonitorContext _orderMonitorContext;

        public OrderMonitorRepo(ILogger<OrderMonitorRepo> logger, OrderMonitorContext orderMonitorContext) 
        {
            _logger = logger;
            _orderMonitorContext = orderMonitorContext;
        }

        public async Task UpdateOrderRecordAsync(DeliveryNotification deliveryNotification)
        {
            try
            {
                var currentOrder = _orderMonitorContext.Orders.FirstOrDefault(o => o.OrderId == deliveryNotification.OrderId);

                if (currentOrder != null)
                    currentOrder.OrderStatus = "Notification Sent";

                await _orderMonitorContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Error: Unable to update record for Order Id - {deliveryNotification.OrderId}");
                throw new ApplicationException("Unable to update record from OrderMonitorRepo", ex);
            }            
        }
    }
}
