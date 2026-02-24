using Basket.API.Infrastructure;
using Basket.API.Models;
using Common.Kernel.CQRS.Commands;

namespace Basket.API.ShoppingBasket.Save
{
    public class SaveCartCommandHandler(ICartRepository cartRepository) : ICommandHandler<SaveCartCommand, SaveCartResult>
    {
        public async Task<SaveCartResult> Handle(SaveCartCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.Cart;
            await cartRepository.SaveCartAsync(cart, cancellationToken);
            return new SaveCartResult(cart.AccountName);
        }
    }
}
