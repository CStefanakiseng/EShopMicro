using EShopMicro.Discount.Grpc.Protos;

namespace EShopMicro.Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountServiceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountServiceClient)
        {
            _discountServiceClient = discountServiceClient ?? throw new ArgumentNullException(nameof(discountServiceClient));
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await _discountServiceClient.GetDiscountAsync(discountRequest);
        }
    }
}
