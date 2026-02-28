using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Infrastructure
{
    public class RedisCartCacheRepository(ICartRepository cartRepository, IDistributedCache distributedCache)
        : ICartRepository
    {
        public async Task<ShoppingCart> GetCartAsync(string accountName, CancellationToken cancellationToken)
        {
            string? cached = await distributedCache.GetStringAsync(accountName, cancellationToken);

            if (!String.IsNullOrEmpty(cached))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cached)!;
            }

            var cart = await cartRepository.GetCartAsync(accountName, cancellationToken);
            await distributedCache.SetStringAsync(accountName,JsonSerializer.Serialize(cart), cancellationToken);
            return cart;
        }

        public async Task<bool> RemoveCartAsync(string accountName, CancellationToken cancellationToken)
        {
            var result = await cartRepository.RemoveCartAsync(accountName, cancellationToken);
            await distributedCache.RemoveAsync(accountName, cancellationToken);
            return result;
        }

        public async Task<ShoppingCart> SaveCartAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken)
        {
            var result = await cartRepository.SaveCartAsync(shoppingCart, cancellationToken);
            await distributedCache.SetStringAsync(shoppingCart.AccountName, JsonSerializer.Serialize(shoppingCart), cancellationToken);
            return result;
        }
    }
}
