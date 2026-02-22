using Catalog.Domain.Repositories;
using Catalog.Domain.Specifications;
using Marten;

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
        public async Task<Pagination<CatalogItem>> GetCatalogItemsAsync(QueryArgs args)
        {
            var allItems = _documentSession.Query<CatalogItem>().AsQueryable();

            var brandId = args.BrandId;
            if (brandId is not null)
            {
                allItems = allItems.Where(i => i.Brand != null && i.Brand.Id == brandId);
            }

            var categoryId = args.CategoryId;
            if (categoryId is not null)
            {
                allItems = allItems.Where(i => i.Category != null && i.Category.Id == categoryId);
            }

            var search = args.Search;
            if (!String.IsNullOrEmpty(search))
            {
                allItems = allItems.Where(
                    i => i.Title != null
                    && i.Title.Contains(search, StringComparison.OrdinalIgnoreCase)
                );
            }

            if (!string.IsNullOrEmpty(args.Sort))
            {
                allItems = args.Sort.ToLower() switch
                {
                    "price_desc" => allItems.OrderByDescending(i => i.Price),
                    "price_asc" => allItems.OrderBy(i => i.Price),
                    "title_desc" => allItems.OrderByDescending(i => i.Title),
                    "title_asc" => allItems.OrderBy(i => i.Title),
                    _ => allItems
                };
            }

            var count = await allItems.CountAsync();

            var items = await allItems.Skip((args.PageIndex - 1) * args.PageSize)
                .Take(args.PageSize).ToListAsync();

            return new Pagination<CatalogItem>(
                args.PageIndex,
                args.PageSize,
                count,
                items
            );
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
