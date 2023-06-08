using EShopMicro.WebApp.Models;

namespace EShopMicro.WebApp.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}
