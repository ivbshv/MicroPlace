namespace Promotion.Grpc.Persistence.Interfaces
{
    public interface IPromoRepository
    {
        Task<Promo?> GetByCatalogItemIdAsync(string? catalogItemId, CancellationToken cancellationToken);
    }
}
