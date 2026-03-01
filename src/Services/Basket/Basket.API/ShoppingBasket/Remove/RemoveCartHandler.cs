using Basket.API.Infrastructure;
using Common.Kernel.CQRS.Commands;

namespace Basket.API.ShoppingBasket.Remove
{
    public class RemoveCartHandler(ICartRepository cartRepository) : ICommandHandler<RemoveCartCommand, RemoveCartResult>
    {
        public async Task<RemoveCartResult> Handle(RemoveCartCommand command, CancellationToken cancellationToken)
        {
            var result = await cartRepository.RemoveCartAsync(command.AccountName, cancellationToken);
            return new RemoveCartResult(result);
        }
    }
}
