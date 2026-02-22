using Catalog.Application.Queries.CatalogItemQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemHandlers
{
    public class GetCatalogItemByTitleQueryHandlers(ICatalogItemRepository catalogItemRepository)
        : IRequestHandler<GetCatalogItemByTitleQuery, GetCatalogItemByTitleResult>
    {
        public async Task<GetCatalogItemByTitleResult> Handle(GetCatalogItemByTitleQuery query, CancellationToken cancellationToken)
        {
            var catalogItems = await catalogItemRepository.GetCatalogItemsByTitleAsync(query.Title);
            var result = new GetCatalogItemByTitleResult(catalogItems);
            return result;
        }
    }
}
