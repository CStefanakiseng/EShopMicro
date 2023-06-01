using MediatR;

namespace EShopMicro.Ordering.Application.Features.Orders.Queries.List
{
    public class GetOrdersListQuery : IRequest<List<OrderViewModel>>
    {
        public string UserName { get; set; }

        public GetOrdersListQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}