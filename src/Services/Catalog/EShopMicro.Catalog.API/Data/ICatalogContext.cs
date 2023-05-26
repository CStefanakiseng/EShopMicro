using EShopMicro.Catalog.API.Entities;
using MongoDB.Driver;

namespace EShopMicro.Catalog.API.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
