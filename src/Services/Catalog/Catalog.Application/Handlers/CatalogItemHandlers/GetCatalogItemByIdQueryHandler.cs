using Catalog.Application.Queries.CatalogItemQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemHandlers
{
    public class GetCatalogItemByIdQueryHandler(ICatalogItemRepository catalogItemRepository)
        : IRequestHandler<GetCatalogItemByIdQuery, GetCatalogItemByIdResult>
    {
        public async Task<GetCatalogItemByIdResult> Handle(GetCatalogItemByIdQuery query, CancellationToken cancellationToken)
        {
            var catalogItem = await catalogItemRepository.GetCatalogItemAsync(query.Id);
            var result = new GetCatalogItemByIdResult(catalogItem);
            return result;
        }
    }
}
