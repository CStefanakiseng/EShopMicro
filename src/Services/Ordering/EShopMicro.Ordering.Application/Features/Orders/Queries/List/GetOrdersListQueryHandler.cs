using AutoMapper;
using EShopMicro.Ordering.Application.Contracts.Persistence;
using MediatR;

namespace EShopMicro.Ordering.Application.Features.Orders.Queries.List
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderViewModel>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<OrderViewModel>> Handle(GetOrdersListQuery request,
            CancellationToken cancellationToken)
        {
            var orderList = await _orderRepository.GetOrdersByUserName(request.UserName);
            return _mapper.Map<List<OrderViewModel>>(orderList);
        }
    }
}