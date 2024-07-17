using Microsoft.AspNetCore.Mvc;
using SynapseHealthOrderMonitorAPI.Models;
using SynapseHealthOrderMonitorAPI.Services;

namespace SynapseHealthOrderMonitorAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderMonitorController : ControllerBase
    {
        private readonly ILogger<OrderMonitorController> _logger;
        private readonly IOrderMonitorService _orderMonitorService;

        public OrderMonitorController(ILogger<OrderMonitorController> logger, IOrderMonitorService orderMonitorService)
        {
            _logger = logger;
            _orderMonitorService = orderMonitorService;
        }

        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [HttpPost("processOrders")]
        public async Task<IActionResult> ProcessOrders([FromBody] IEnumerable<Order> ordersRequest)
        {
            try
            {
                await _orderMonitorService.ProcessOrderAsync(ordersRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error");
                return NotFound(ex.Message);
            }
        }

        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
