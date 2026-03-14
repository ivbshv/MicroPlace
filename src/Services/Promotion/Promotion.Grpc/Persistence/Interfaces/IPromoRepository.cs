namespace Promotion.Grpc.Persistence.Interfaces
{
    public interface IPromoRepository
    {
        Task<Promo?> GetByCatalogItemIdAsync(string catalogItemId, CancellationToken cancellationToken);
        Task<bool> CreateAsync(Promo promo, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Promo promo, CancellationToken cancellationToken);
    }
}
