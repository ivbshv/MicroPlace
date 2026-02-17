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
    public class GetCatalogItemsQueryHandler(ICatalogItemRepository catalogItemRepository)
        : IRequestHandler<GetCatalogItemsQuery, GetCatalogItemsResult>
    {
        public async Task<GetCatalogItemsResult> Handle(GetCatalogItemsQuery query, CancellationToken cancellationToken)
        {
            var catalogItems = await catalogItemRepository.GetAllCatalogItemsAsync();
            var result = new GetCatalogItemsResult(catalogItems);
            return result;

        }
    }
}
