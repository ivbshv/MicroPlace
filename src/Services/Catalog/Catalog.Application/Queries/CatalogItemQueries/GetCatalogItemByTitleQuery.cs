using Catalog.Application.Responses.CatalogItemResponses;
using MediatR;

namespace Catalog.Application.Queries.CatalogItemQueries
{
    public record GetCatalogItemByTitleQuery(string Title) : IRequest<GetCatalogItemByTitleResult>
    {
    }
}
