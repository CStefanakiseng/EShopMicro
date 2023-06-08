using EShopMicro.ShoppingAggregator.Models;

namespace EShopMicro.ShoppingAggregator.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}
