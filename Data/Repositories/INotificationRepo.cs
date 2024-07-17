using SynapseHealthOrderMonitorAPI.Models;

namespace SynapseHealthOrderMonitorAPI.Data.Repositories
{
    public interface INotificationRepo
    {
        Task<bool> SendNotificationAsync(Order order);
    }
}
