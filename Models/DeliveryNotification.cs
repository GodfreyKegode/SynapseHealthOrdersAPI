namespace SynapseHealthOrderMonitorAPI.Models
{
    public class DeliveryNotification
    {
        public int NotificationId { get; set; }
        public int OrderId { get; set; }
        public string? Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}
