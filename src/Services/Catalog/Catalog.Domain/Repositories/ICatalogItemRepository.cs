namespace Catalog.Domain.Repositories
{
    internal interface ICatalogItemRepository
    {
        Task<CatalogItem> CreatecatalogItemAsync(CatalogItem item);
        Task<IEnumerable<CatalogItem>> GetAllCatalogItemsAsync();
        Task<CatalogItem?> GetCatalogItemAsync(CatalogItem item);
        Task<IEnumerable<CatalogItem>> GetCatalogItemsByTitleAsync(string title);
        Task<IEnumerable<CatalogItem>> GetCatalogItemsByBrandsAsync(string brandTitle);
        Task<bool> UpdateCatalogItemAsync(CatalogItem item);
        Task<bool> DeleteCatalogItemAsync(Guid id);



    }
}
