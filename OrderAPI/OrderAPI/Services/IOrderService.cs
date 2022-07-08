using OrderAPI.Models;

namespace OrderAPI.Services
{
    public interface IOrderService
    {
       async Task<string> SendOrder(Order Order);
    }
}
