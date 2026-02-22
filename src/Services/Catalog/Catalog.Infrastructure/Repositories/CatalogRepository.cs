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
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            return await _documentSession.Query<Category>().ToListAsync(cancellationToken);
        }

        // IBrandRepository
        public async Task<IEnumerable<Brand>> GetAllBrandsAsync(CancellationToken cancellationToken)
        {
            return await _documentSession.Query<Brand>().ToListAsync(cancellationToken);
        }

        // ICatalogItemRepository

        public async Task<IEnumerable<CatalogItem>> GetAllCatalogItemsAsync(CancellationToken cancellationToken)
        {
             return await _documentSession.Query<CatalogItem>().ToListAsync(cancellationToken);
        }

        public async Task<CatalogItem?> GetCatalogItemAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _documentSession.LoadAsync<CatalogItem>(id, cancellationToken);
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItemsByBrandsAsync(string brandTitle, CancellationToken cancellationToken)
        {
            return await _documentSession.Query<CatalogItem>().Where(x => x.Brand != null && !String.IsNullOrEmpty(x.Brand.Title) 
            && x.Brand.Title.Contains(brandTitle, StringComparison.OrdinalIgnoreCase)).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItemsByTitleAsync(string title, CancellationToken cancellationToken)
        {
            return await _documentSession.Query<CatalogItem>().Where(x => !String.IsNullOrEmpty(x.Title) 
            && x.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToListAsync(cancellationToken);
        }
        public async Task<Pagination<CatalogItem>> GetCatalogItemsAsync(QueryArgs args, CancellationToken cancellationToken)
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

            var count = await allItems.CountAsync(cancellationToken);

            var items = await allItems.Skip((args.PageIndex - 1) * args.PageSize)
                .Take(args.PageSize).ToListAsync(cancellationToken);

            return new Pagination<CatalogItem>(
                args.PageIndex,
                args.PageSize,
                count,
                items
            );
        }
        public async Task<CatalogItem> CreateCatalogItemAsync(CatalogItem item, CancellationToken cancellationToken)
        {
            _documentSession.Store(item);
            await _documentSession.SaveChangesAsync(cancellationToken);
            return item;
        }

        public async Task<bool> DeleteCatalogItemAsync(Guid id, CancellationToken cancellationToken)
        {
            _documentSession.Delete<CatalogItem>(id);
            await _documentSession.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateCatalogItemAsync(CatalogItem item, CancellationToken cancellationToken)
        {
            _documentSession.Store(item);
            await _documentSession.SaveChangesAsync(cancellationToken);
            return true;
        }

    }
}
