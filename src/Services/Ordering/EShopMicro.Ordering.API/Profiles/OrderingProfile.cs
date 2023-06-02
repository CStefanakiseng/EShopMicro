using AutoMapper;
using EShopMicro.EventBus.Messages.Events;
using EShopMicro.Ordering.Application.Features.Orders.Commands.Checkout;

namespace EShopMicro.Ordering.API.Profiles
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<BasketCheckoutEvent, CheckoutOrderCommand>().ReverseMap();
        }
    }
}
