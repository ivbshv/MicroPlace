using Catalog.Application.Queries.CatalogItemQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemHandlers
{
    public class GetCatalogItemByBrandTitleQueryHandler(ICatalogItemRepository catalogItemRepository)
        : IRequestHandler<GetCatalogItemByBrandTitleQuery, GetCatalogItemByBrandTitleResult>
    {
        public async Task<GetCatalogItemByBrandTitleResult> Handle(GetCatalogItemByBrandTitleQuery query, CancellationToken cancellationToken)
        {
            var catalogItem = await catalogItemRepository.GetCatalogItemsByBrandsAsync(query.BrandTitle);
            var result = new GetCatalogItemByBrandTitleResult(catalogItem);
            return result;
        }
    }
}
