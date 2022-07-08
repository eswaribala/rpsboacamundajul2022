using Newtonsoft.Json;
using OrderAPI.Models;
using System.Text;

namespace OrderAPI.Services
{
    public class OrderService : IOrderService
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;


        public OrderService(IConfiguration configuration,HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<string> SendOrder(Order Order)
        {

            Root root = new Root()
            {
                MessageName = _configuration["MessageName"],
                BusinessKey = new Random().Next().ToString(),
                ProcessVariables = new ProcessVariables()
                {
                    OrderId = new OrderId()
                    {
                        Value = Order.OrderId.ToString(),
                        Type = "String"
                    },
                    OrderAmount = new OrderAmount()
                    {
                        Value = Order.OrderAmount.ToString(),
                        Type = "String"
                    },
                    OrderDate = new OrderDate()
                    {
                        Value = Order.OrderDate.ToString(),
                        Type = "String"
                    }
                }
            };
            var json = JsonConvert.SerializeObject(root);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            
            _httpClient=new HttpClient();
          var response= await _httpClient.PostAsync(_configuration["RestApiUri"], data);

           return await  response.Content.ReadAsStringAsync();
        }
    }
}
