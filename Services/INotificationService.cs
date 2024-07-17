using SynapseHealthOrderMonitorAPI.Models;

namespace SynapseHealthOrderMonitorAPI.Services
{
    public interface INotificationService
    {
        Task<bool> SendDeliveryNotificationAsync(Order order);
    }
}
