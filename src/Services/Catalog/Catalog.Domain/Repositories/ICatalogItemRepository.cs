using Catalog.Domain.Specifications;

namespace Catalog.Domain.Repositories
{
    public interface ICatalogItemRepository
    {
        Task<CatalogItem> CreateCatalogItemAsync(CatalogItem item);
        Task<IEnumerable<CatalogItem>> GetAllCatalogItemsAsync();
        Task<CatalogItem?> GetCatalogItemAsync(Guid id);
        Task<IEnumerable<CatalogItem>> GetCatalogItemsByTitleAsync(string title);
        Task<IEnumerable<CatalogItem>> GetCatalogItemsByBrandsAsync(string brandTitle);
        Task<Pagination<CatalogItem>> GetCatalogItemsAsync(QueryArgs args);
        Task<bool> UpdateCatalogItemAsync(CatalogItem item);
        Task<bool> DeleteCatalogItemAsync(Guid id);
    }
}
