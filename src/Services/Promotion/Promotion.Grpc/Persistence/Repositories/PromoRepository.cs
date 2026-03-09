
namespace Promotion.Grpc.Persistence.Repositories
{
    public class PromoRepository(IDbConnection connection) : IPromoRepository
    {
        public async Task<Promo?> GetByCatalogItemIdAsync(string? catalogItemId, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(catalogItemId);

            const string query = """
                SELECT * FROM Promo
                WHERE CatalogItemId = @catalogItemId
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

        public async Task<bool> CreateAsync(Promo? promo, CancellationToken cancellationToken)
        {
            const string query = """
                INSERT INTO Promo (Id,CatalogItemId, Title, Value)
                VALUES (@Id, @CatalogItemId, @Title, @Value);
            """;

            var result = await connection.ExecuteAsync(query, promo);
            return result > 0;
        }

    }
}
