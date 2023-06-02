using AutoMapper;
using EShopMicro.Basket.API.Entities;
using EShopMicro.EventBus.Messages.Events;

namespace EShopMicro.Basket.API.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
