using AutoMapper;
using EShopMicro.Ordering.Application.Features.Orders.Commands.Checkout;
using EShopMicro.Ordering.Application.Features.Orders.Commands.Update;
using EShopMicro.Ordering.Application.Features.Orders.Queries;
using EShopMicro.Ordering.Domain.Entities;

namespace EShopMicro.Ordering.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}