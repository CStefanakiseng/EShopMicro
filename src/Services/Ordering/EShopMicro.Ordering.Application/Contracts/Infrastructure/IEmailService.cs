using EShopMicro.Ordering.Application.Models;

namespace EShopMicro.Ordering.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}