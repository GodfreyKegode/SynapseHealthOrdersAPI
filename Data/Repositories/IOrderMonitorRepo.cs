using SynapseHealthOrderMonitorAPI.Models;

namespace SynapseHealthOrderMonitorAPI.Data.Repositories
{
    public interface IOrderMonitorRepo
    {
        Task UpdateOrderRecordAsync(DeliveryNotification deliveryNotification);
    }
}
