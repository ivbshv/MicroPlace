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
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _documentSession.Query<Category>().ToListAsync();
        }

        // IBrandRepository
        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _documentSession.Query<Brand>().ToListAsync();
        }

        // ICatalogItemRepository

        public async Task<IEnumerable<CatalogItem>> GetAllCatalogItemsAsync()
        {
             return await _documentSession.Query<CatalogItem>().ToListAsync();
        }

        public async Task<CatalogItem?> GetCatalogItemAsync(Guid id)
        {
            return await _documentSession.LoadAsync<CatalogItem>(id);
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItemsByBrandsAsync(string brandTitle)
        {
            return await _documentSession.Query<CatalogItem>().Where(x => x.Brand != null && !String.IsNullOrEmpty(x.Brand.Title) 
            && x.Brand.Title.Contains(brandTitle, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItemsByTitleAsync(string title)
        {
            return await _documentSession.Query<CatalogItem>().Where(x => !String.IsNullOrEmpty(x.Title) 
            && x.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }
        public async Task<CatalogItem> CreateCatalogItemAsync(CatalogItem item)
        {
            _documentSession.Store(item);
            await _documentSession.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteCatalogItemAsync(Guid id)
        {
            _documentSession.Delete<CatalogItem>(id);
            await _documentSession.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCatalogItemAsync(CatalogItem item)
        {
            _documentSession.Store(item);
            await _documentSession.SaveChangesAsync();
            return true;
        }
    }
}
