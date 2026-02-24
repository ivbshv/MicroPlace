using Basket.API.Models;
using Marten;
using Microsoft.Extensions.Logging;

namespace Basket.API.Infrastructure
{
    public class CartRepository(IDocumentSession session) : ICartRepository
    {
        public async Task<ShoppingCart> GetCartAsync(string accountName, CancellationToken cancellationToken)
        {
            var cart = await session.LoadAsync<ShoppingCart>(accountName, cancellationToken);

            if (cart is null)
            {
                throw new Exception($"Корзина для '{accountName}' не найдена");
            }

            return cart;
        }

        public async Task<bool> RemoveCartAsync(string accountName, CancellationToken cancellationToken)
        {
            var cart = await session.LoadAsync<ShoppingCart>(accountName, cancellationToken);

            if (cart is null)
            {
                throw new Exception($"Корзина для '{accountName}' не найдена");
            }

            session.Delete<ShoppingCart>(accountName);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<ShoppingCart> SaveCartAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken)
        {
            session.Store(shoppingCart);
            await session.SaveChangesAsync(cancellationToken);
            return shoppingCart;
        }
    }
}
