using Catalog.Domain.Repositories;
using Marten;
using NetTopologySuite.Index.HPRtree;

namespace Catalog.Infrastructure.Repositories
{
    public class CatalogRepository : IBrandRepository, ICategoryRepository, ICatalogItemRepository
    {
        private readonly IDocumentSession _documentSession;
        public CatalogRepository(IDocumentSession session) {
            _documentSession = session;
        }

        // ICategoryRepository
        async Task<IEnumerable<Category>> ICategoryRepository.GetAllCategoriesAsync()
        {
            return await _documentSession.Query<Category>().ToListAsync();
        }

        // IBrandRepository
        async Task<IEnumerable<Brand>> IBrandRepository.GetAllBrandsAsync()
        {
            return await _documentSession.Query<Brand>().ToListAsync();
        }

        // ICatalogItemRepository

        async Task<IEnumerable<CatalogItem>> ICatalogItemRepository.GetAllCatalogItemsAsync()
        {
             return await _documentSession.Query<CatalogItem>().ToListAsync();
        }

        async Task<CatalogItem?> ICatalogItemRepository.GetCatalogItemAsync(CatalogItem item)
        {
            return await _documentSession.Query<CatalogItem>().FirstOrDefaultAsync(i => i.Id == item.Id);
        }

        async Task<IEnumerable<CatalogItem>> ICatalogItemRepository.GetCatalogItemsByBrandsAsync(string brandTitle)
        {
            return await _documentSession.Query<CatalogItem>().Where(x => x.Brand.Title == brandTitle).ToListAsync();
        }

        async Task<IEnumerable<CatalogItem>> ICatalogItemRepository.GetCatalogItemsByTitleAsync(string title)
        {
            return await _documentSession.Query<CatalogItem>().Where(x => x.Title.Contains(title)).ToListAsync();
        }
        async Task<CatalogItem> ICatalogItemRepository.CreateCatalogItemAsync(CatalogItem item)
        {
            _documentSession.Store(item);
            await _documentSession.SaveChangesAsync();
            return item;
        }

        async Task<bool> ICatalogItemRepository.DeleteCatalogItemAsync(Guid id)
        {
            var existingItem = await _documentSession.Query<CatalogItem>().FirstOrDefaultAsync(x => x.Id == id);

            if(existingItem == null) return false;

            _documentSession.Delete(existingItem);

            _documentSession.SaveChangesAsync();

            return true;
        }

        async Task<bool> ICatalogItemRepository.UpdateCatalogItemAsync(CatalogItem item)
        {
            var existingItem = await _documentSession.Query<CatalogItem>().FirstOrDefaultAsync(x => x.Id == item.Id);

            if(existingItem == null) return false;

            _documentSession.Update(existingItem);

            _documentSession.SaveChangesAsync();

            return true;
        }
    }
}
