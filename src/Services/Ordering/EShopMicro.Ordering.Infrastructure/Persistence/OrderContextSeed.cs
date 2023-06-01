using EShopMicro.Ordering.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace EShopMicro.Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "stcFromCon", FirstName = "Christos", LastName = "Stefanakis", EmailAddress = "cstefanakiseng@gmail.com", AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350 }
            };
        }
    }
}