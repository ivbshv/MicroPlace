using Catalog.Application.Responses.CatalogItemResponses;
using MediatR;

namespace Catalog.Application.Queries.CatalogItemQueries
{
    public record GetCatalogItemByBrandTitleQuery(string BrandTitle) : IRequest<GetCatalogItemByBrandTitleResult>
    {
    }
}
