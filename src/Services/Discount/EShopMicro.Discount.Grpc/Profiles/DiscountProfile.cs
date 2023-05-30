using AutoMapper;
using EShopMicro.Discount.Grpc.Entities;
using EShopMicro.Discount.Grpc.Protos;

namespace EShopMicro.Discount.Grpc.Profiles
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
