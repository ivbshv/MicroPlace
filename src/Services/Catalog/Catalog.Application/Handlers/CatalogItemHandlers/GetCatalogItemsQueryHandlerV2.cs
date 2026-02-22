using Catalog.Application.Queries.CatalogItemQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CatalogItemHandlers
{
    public class GetCatalogItemsQueryHandlerV2(ICatalogItemRepository catalogItemRepository)
        : IRequestHandler<GetCatalogItemsQueryV2, GetCatalogItemsResultV2>
    {
        public async Task<GetCatalogItemsResultV2> Handle(GetCatalogItemsQueryV2 query, CancellationToken cancellationToken)
        {
            var pagination = await catalogItemRepository.GetCatalogItemsAsync(query.Args, cancellationToken);

            return new GetCatalogItemsResultV2(pagination);
        }
    }
}
