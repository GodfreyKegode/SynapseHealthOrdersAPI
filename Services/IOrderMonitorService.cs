using SynapseHealthOrderMonitorAPI.Models;

namespace SynapseHealthOrderMonitorAPI.Services
{
    public interface IOrderMonitorService
    {
        Task ProcessOrderAsync(IEnumerable<Order> orders);
    }
}
