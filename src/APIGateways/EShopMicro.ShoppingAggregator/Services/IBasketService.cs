using EShopMicro.ShoppingAggregator.Models;

namespace EShopMicro.ShoppingAggregator.Services
{
    public interface IBasketService
    {
        Task<BasketModel> GetBasket(string userName);
    }
}
