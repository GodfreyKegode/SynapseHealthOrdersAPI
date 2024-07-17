
using SynapseHealthOrderMonitorAPI.Data.Repositories;
using SynapseHealthOrderMonitorAPI.Models;

namespace SynapseHealthOrderMonitorAPI.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly INotificationRepo _notificationRepo;

        public NotificationService(ILogger<NotificationService> logger, INotificationRepo notificationRepo) 
        {
            _logger = logger;
            _notificationRepo = notificationRepo;
        }

        // Dispatch delivery notification
        public async Task<bool> SendDeliveryNotificationAsync(Order order)
        {
            try
            {
                bool notificationDelivered = false;
                var sendNotification = await _notificationRepo.SendNotificationAsync(order);

                if (sendNotification)
                    notificationDelivered = true;

                return notificationDelivered;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error");
                throw new ApplicationException("Unable to send notification from NotificationService", ex);
            }

            
        }
    }
}
