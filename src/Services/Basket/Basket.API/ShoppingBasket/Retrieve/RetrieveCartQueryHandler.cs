using Basket.API.Infrastructure;
using Common.Kernel.CQRS.Queries;

namespace Basket.API.ShoppingBasket.Retrieve
{
    public class RetrieveCartQueryHandler(ICartRepository cartRepository) : IQueryHandler<RetrieveCartQuery, RetrieveCartResult>
    {
        public async Task<RetrieveCartResult> Handle(RetrieveCartQuery query, CancellationToken cancellationToken)
        {
            var cart = await cartRepository.GetCartAsync(query.AccountName, cancellationToken);
            return new RetrieveCartResult(cart);
        }
    }
}
