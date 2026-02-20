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
            var Allitems = await catalogItemRepository.GetAllCatalogItemsAsync();

            var count = Allitems.Count();

            var items = Allitems.Skip((query.PageIndex - 1) * query.PageSize)
                .Take(query.PageSize).ToList();

            var pagination = new Pagination<CatalogItem>(
                PageIndex: query.PageIndex,
                PageSize: query.PageSize,
                TotalCount: count,
                Items: items
            );

            return new GetCatalogItemsResultV2(pagination);
        }
    }
}
