using Newtonsoft.Json;
using OrderAPI.Models;
using System.Text;

namespace OrderAPI.Services
{
    public class OrderService : IOrderService
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;
        private ILogger<OrderService> _logger;

        public OrderService(IConfiguration configuration,HttpClient httpClient,ILogger<OrderService> logger)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<string> SendOrder(Order Order)
        {

            Root root = new Root()
            {
                messageName = _configuration["MessageName"],
                businessKey = new Random().Next().ToString(),
                processVariables = new ProcessVariables()
                {
                    OrderId = new OrderId()
                    {
                        value = Order.OrderId.ToString(),
                        type = "String"
                    },
                    OrderAmount = new OrderAmount()
                    {
                        value = Order.OrderAmount.ToString(),
                        type = "String"
                    },
                    OrderDate = new OrderDate()
                    {
                        value = Order.OrderDate.ToString(),
                        type = "String"
                    }
                }
            };
            var json = JsonConvert.SerializeObject(root);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            
            _httpClient=new HttpClient();
          var response= await _httpClient.PostAsync(_configuration["RestApiUri"], data);

           return await response.Content.ReadAsStringAsync();
        }
    }
}
