using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Models;
using OrderAPI.Services;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private ILogger<OrdersController> _logger;
        private IOrderService _orderService;
        public OrdersController(IOrderService orderService,ILogger<OrdersController> logger)
        {
            _logger = logger;
            _orderService = orderService;
        }

        // POST api/<VehicleController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order Order)
        {

            _logger.LogInformation($"The Order received as {Order.OrderId}");
           var result= await _orderService.SendOrder(Order);
            return Ok(result);
            

        }
    }
}
