using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Models;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private ILogger<OrdersController> _logger;
        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        // POST api/<VehicleController>
        [HttpPost]
        public Order Post([FromBody] Order Order)
        {

            _logger.LogInformation($"The Order received as {Order.OrderId}");
            return Order;

        }
    }
}
