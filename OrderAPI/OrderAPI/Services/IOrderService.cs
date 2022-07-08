using OrderAPI.Models;

namespace OrderAPI.Services
{
    public interface IOrderService
    {
      Task<string> SendOrder(Order Order);
    }
}
