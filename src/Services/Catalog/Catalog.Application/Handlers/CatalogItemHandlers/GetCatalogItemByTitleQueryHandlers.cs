using Catalog.Application.Queries.CatalogItemQueries;
using Catalog.Application.Responses.CatalogItemResponses;
using Catalog.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers.CatalogItemHandlers
{
    public class GetCatalogItemByTitleQueryHandlers(ICatalogItemRepository catalogItemRepository)
        : IRequestHandler<GetCatalogItemByTitleQuery, GetCatalogItemByTitleResult>
    {
        public async Task<GetCatalogItemByTitleResult> Handle(GetCatalogItemByTitleQuery query, CancellationToken cancellationToken)
        {
            var catalogItem = await catalogItemRepository.GetCatalogItemsByTitleAsync(query.Title);
            var result = new GetCatalogItemByTitleResult(catalogItem);
            return result;
        }
    }
}
