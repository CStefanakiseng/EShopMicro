﻿using EShopMicro.Ordering.Application.Contracts.Persistence;
using EShopMicro.Ordering.Domain.Entities;
using EShopMicro.Ordering.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EShopMicro.Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var orderList = await _dbContext.Orders
                                    .Where(o => o.UserName == userName)
                                    .ToListAsync();
            return orderList;
        }
    }
}