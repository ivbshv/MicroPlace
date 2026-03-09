
namespace Promotion.Grpc.Persistence.Repositories
{
    public class PromoRepository(IDbConnection connection) : IPromoRepository
    {
        public async Task<Promo?> GetByCatalogItemIdAsync(string? catalogItemId, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(catalogItemId);

            const string query = """
                SELECT * FROM Promo
                WHERE catalogItemId = @CatalogItemId
                LIMIT 1;
            """;

            var result = await connection.QueryFirstOrDefaultAsync<Promo>(
                 new CommandDefinition(
                     query,
                     new { CatalogItemId = catalogItemId },
                     cancellationToken: cancellationToken)
                 );

            return result;
        }
    }
}
