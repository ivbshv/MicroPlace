namespace Promotion.Grpc.Persistence.Repositories
{
    public class PromoRepository(IDbConnection connection) : IPromoRepository
    {
        public async Task<Promo?> GetByCatalogItemIdAsync(string catalogItemId, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(catalogItemId);

            const string query = """
                SELECT Id, CatalogItemId, Title, Value
                FROM Promo
                WHERE CatalogItemId = @CatalogItemId
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

        public async Task<bool> CreateAsync(Promo promo, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(promo);
            const string query = """
                INSERT INTO Promo (Id,CatalogItemId, Title, Value)
                VALUES (@Id, @CatalogItemId, @Title, @Value);
            """;

            var result = await connection.ExecuteAsync(
                new CommandDefinition(
                    query,
                    promo,
                    cancellationToken: cancellationToken
                )
            );

            return result > 0;
        }

        public async Task<bool> UpdateAsync(Promo promo, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(promo);

            const string query = """
                UPDATE Promo SET
                    Title = @Title,
                    Value = @Value
                WHERE Id = @Id;
                """;

            var result = await connection.ExecuteAsync(
                new CommandDefinition(
                    query,
                    promo,
                    cancellationToken: cancellationToken
                )
            );

            return result > 0;
        }
    }
}
