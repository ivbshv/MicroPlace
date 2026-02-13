using Marten.Schema;
using Marten;

namespace Catalog.Infrastructure.Data.Seed
{
    public class InitializeDatabaseAsync : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (!await session.Query<Brand>().AnyAsync())
            {
                session.Store<Brand>(InitialData.Brands);
                await session.SaveChangesAsync(cancellation);
            }

            foreach (var category in InitialData.Categories)
            {
                if(!session.Query<Category>().Any(c => c.Id == category.Id))
                {
                    session.Store(category); 
                }
            }

            foreach (var item in InitialData.CatalogItems)
            {
                if (!session.Query<CatalogItem>().Any(c => c.Id == item.Id))
                {
                    session.Store(item); 
                }
            }

            await session.SaveChangesAsync(cancellation);

        }
    }
}
