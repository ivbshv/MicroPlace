using Catalog.Application.Queries.CatalogItemQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Catalog.Domain.Specifications;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemHandlers
{
    public class GetCatalogItemsQueryHandlerV2(ICatalogItemRepository catalogItemRepository)
        : IRequestHandler<GetCatalogItemsQueryV2, GetCatalogItemsResultV2>
    {
        public async Task<GetCatalogItemsResultV2> Handle(GetCatalogItemsQueryV2 query, CancellationToken cancellationToken)
        {
            var allItems = await catalogItemRepository.GetAllCatalogItemsAsync();

            var brandId = query.Args.BrandId;
            if (brandId is not null)
            {
                allItems = allItems.Where(i => i.Brand?.Id == brandId);
            }

            var categoryId = query.Args.CategoryId;
            if (categoryId is not null)
            {
                allItems = allItems.Where(i => i.Category?.Id == categoryId);
            }

            var search = query.Args.Search;
            if (!String.IsNullOrEmpty(search))
            {
                allItems = allItems.Where(
                    i => i.Title != null
                    && i.Title.Contains(search, StringComparison.OrdinalIgnoreCase)
                );
            }

            if (!string.IsNullOrEmpty(query.Args.Sort))
            {
                allItems = query.Args.Sort.ToLower() switch
                {
                    "price_desc" => allItems.OrderByDescending(i => i.Price),
                    "price_asc" => allItems.OrderBy(i => i.Price),
                    "title_desc" => allItems.OrderByDescending(i => i.Title),
                    "title_asc" => allItems.OrderBy(i => i.Title),
                    _ => allItems
                };
            }

            var count = allItems.Count();

            var items = allItems.Skip((query.Args.PageIndex - 1) * query.Args.PageSize)
                .Take(query.Args.PageSize).ToList();

            var pagination = new Pagination<CatalogItem>(
                PageIndex: query.Args.PageIndex,
                PageSize: query.Args.PageSize,
                TotalCount: count,
                Items: items
            );

            return new GetCatalogItemsResultV2(pagination);
        }
    }
}
