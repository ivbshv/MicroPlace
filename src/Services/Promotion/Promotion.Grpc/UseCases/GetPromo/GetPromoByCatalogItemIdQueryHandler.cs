namespace Promotion.Grpc.UseCases.GetPromo
{
    public class GetPromoByCatalogItemIdQueryHandler(IPromoRepository promoRepository)
        : IQueryHandler<GetPromoByCatalogItemIdQuery, PromoModel>
    {
        public async Task<PromoModel> Handle(GetPromoByCatalogItemIdQuery query, CancellationToken cancellationToken)
        {
            var promo = await promoRepository.GetByCatalogItemIdAsync(query.CatalogItemId, cancellationToken);

            if (promo is null)
            {
                throw new RpcException(
                    new Status(StatusCode.NotFound,
                    $"Ничего для {query.CatalogItemId} не найдено"
                ));
            }

            var result = promo.Adapt<PromoModel>();
            return result;
        }
    }
}
